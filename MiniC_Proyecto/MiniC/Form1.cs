using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniC
{
    public partial class frmEditor : Form
    {
        string Archivo; // colocar primero y ya podemos editar
        public frmEditor()
        {
            InitializeComponent();
        }

        private void OpcNuevo_Click(object sender, EventArgs e)
        {
            rtbEditor.Clear(); // limpiar el editor
            Archivo = null; // vaciar string archivo
            frmEditor.ActiveForm.Text = "MiniC | ";
        }

        private void OpcAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog AbrirArchivo = new OpenFileDialog();//crear un nuevo cuadro de dialogo de archivo

            AbrirArchivo.Filter = "MiniC | *.c"; // defino que editor solo podrá trabajar con archivos en c

            if (AbrirArchivo.ShowDialog() == DialogResult.OK) // si la paertura de cuadro de dialogo es exitosa
            {
                Archivo = AbrirArchivo.FileName; // recuperamos el nombre del archivo que queremos abrir

                using (StreamReader sr = new StreamReader(Archivo)) // colocar el nombre de archivo recuperado en el stramreader
                {
                    rtbEditor.Text = sr.ReadToEnd(); // lee del caracter cero al final y pasa al editor
                }

                frmEditor.ActiveForm.Text = "MiniC | " + Archivo; // concatenamos el nombre del cuadro de dialogo con el nombre del archivo
            }
        }

        private void OpcGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog GuardarArchivo = new SaveFileDialog();//crear un nuevo cuadro de dialogo de archivo
            GuardarArchivo.Filter = "MiniC | *.c"; // defino que editor solo podrá trabajar con archivos en c

            if (Archivo != null) // si el archivo no está vacío
            {
                using (StreamWriter sw = new StreamWriter(Archivo))
                {
                    sw.Write(rtbEditor.Text); // si está vacío escribir lo que está en rtbEditor
                }
            }
            else
            {
                if (GuardarArchivo.ShowDialog() == DialogResult.OK) // si la paertura de cuadro de dialogo es exitosa
                {
                    Archivo = GuardarArchivo.FileName; // recuperamos el nombre del archivo que queremos abrir
                    using (StreamWriter sw = new StreamWriter(GuardarArchivo.FileName))
                    {
                        sw.Write(rtbEditor.Text); // si no está vacío escribir lo que está en rtbEditor
                    }
                }
            }
        }

        private void OpcGuardarComo_Click(object sender, EventArgs e)
        {
            SaveFileDialog GuardarComo = new SaveFileDialog() //crear un nuevo cuadro de dialogo de archivo
            {
                Title = "Guardar como: ",
                Filter = "MiniC | *.c",
                AddExtension = true
            }; // colocamos propiedades al cuadro de diálogo

            GuardarComo.ShowDialog(); // mostrar el cuadro de diálogo

            if (Archivo != null && GuardarComo.FileName != String.Empty) // si el archivo no está vacío y le he dado un nombre en .filename
            {
                Archivo = GuardarComo.FileName;
                using (StreamWriter sw = new StreamWriter(GuardarComo.FileName))
                {
                    sw.Write(rtbEditor.Text); // usamos nuevamente el método streamwriter
                    frmEditor.ActiveForm.Text = "MiniC | " + Archivo; // cambiamos el nombre de la ventana editor
                    sw.Close();
                }
            }
        }

        private void OpcSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(); // tarea aumentar lo que sea necesario para que al momento de salir pregunte si desa guardar las modificaciones
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalizarLexico AL = new AnalizarLexico(); // creamos un objeto de nuestro analizador lexico
            List<Tokens> LstTokens = AL.AnalisisLexico(rtbEditor.Text); // Le pasamos el archivo para crear una lista de tokens

            rtbEditor.Text += "\n--------------------------------------------------------------------\n";
            for (int i = 0; i < LstTokens.Count; i++)
                rtbEditor.Text += "Linea: " + LstTokens[i].Linea.ToString() +  "    " +
                    "Lexema: " + LstTokens[i].Lexema + "    " +
                    "Token: " + LstTokens[i].Token + "\n";

            // ahora ejecutemos el analisis sintactico
            AnalizadorSintactico AS = new AnalizadorSintactico();
            AS.AnalisisSintactico(LstTokens);

            rtbEditor.Text += "\n--------------------------------------------------------------------\n";
            rtbEditor.Text += "Analisis sintactico completado correctamente.";

        }
    }
}
