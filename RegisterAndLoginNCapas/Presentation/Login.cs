using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion;
using Domain.Models;

namespace Presentation
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void lblGoResgister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
            register.FormClosed += CloseForm;
        }

        private void CloseForm(object sender, FormClosedEventArgs e)
        {
            this.Show();
            lblError.Visible = false;
            txtUsername.Focus();
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        int intentosFallidos = 0;

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

    }
}
