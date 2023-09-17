using System;
using System.Windows.Forms;
using CapaPresentacion;
using Domain.Models;

namespace Presentation
{
    public partial class Login : Form
    {
        private int intentosFallidos = 0;
        private bool cuentaBloqueadaTemp = false;
        private bool cuentaBloqueadaPerm = false;
        private Timer unlockTimer; // Temporizador para desbloqueo temporal
        private Timer permanentLockTimer; // Temporizador para bloqueo permanente

        public Login()
        {
            InitializeComponent();
            InitializeTimers();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void lblGoResgister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
            register.FormClosed += CloseForm;
        }

        private void InitializeTimers()
        {
            // Configurar el temporizador para desbloquear después de 5 segundos
            unlockTimer = new Timer();
            unlockTimer.Interval = 5000; // 5 segundos en milisegundos
            unlockTimer.Tick += UnlockTimer_Tick;

            // Configurar el temporizador para el bloqueo permanente después de 10 intentos fallidos (24 horas en milisegundos)
            permanentLockTimer = new Timer();
            permanentLockTimer.Interval = 24 * 60 * 60 * 1000; // 24 horas en milisegundos
            permanentLockTimer.Tick += PermanentLockTimer_Tick;
        }

        private void UnlockTimer_Tick(object sender, EventArgs e)
        {
            // Desbloquear la cuenta después de 5 segundos
            cuentaBloqueadaTemp = false;
            lblError.Visible = false;
            unlockTimer.Stop(); // Detener el temporizador
        }

        private void PermanentLockTimer_Tick(object sender, EventArgs e)
        {
            // Bloquear la cuenta permanentemente después de 10 intentos fallidos
            cuentaBloqueadaPerm = true;
            lblError.Text = "Cuenta bloqueada permanentemente.";
            lblError.Visible = true;
            btnLogin.Enabled = false; // Desactivar el botón de inicio de sesión
            permanentLockTimer.Stop(); // Detener el temporizador permanente
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (intentosFallidos < 3) // Verifica que no se hayan alcanzado 3 intentos fallidos
            {
                if (txtUsername.Text != "")
                {
                    if (txtPassword.Text != "")
                    {
                        DLoginUser UserModel = new DLoginUser();
                        var validete = UserModel.Login(txtUsername.Text, txtPassword.Text);
                        if (validete)
                        {
                            this.Hide();
                            Form1 application = new Form1();
                            application.Show();
                            application.FormClosed += CloseForm;

                            // Reiniciar los intentos fallidos después de un inicio de sesión exitoso
                            intentosFallidos = 0;
                        }
                        else
                        {
                            lblError.Text = "Credenciales incorrectas!";
                            lblError.Visible = true;

                            // Incrementar el contador de intentos fallidos
                            intentosFallidos++;
                        }
                    }
                    else
                    {
                        lblError.Text = "Contraseña incorrecta";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Usuario incorrecto!";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Has alcanzado el límite de intentos. Cuenta bloqueada.";
                lblError.Visible = true;
            }
        }



        private void CloseForm(object sender, FormClosedEventArgs e)
        {
            this.Show();
            lblError.Visible = false;
            txtUsername.Focus();
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
