using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Arbol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            string filePath = @"C:\Users\andre\Desktop\UNIVERSIDAD\Semestre 6\Interfaces\ArchivosPrueba\Arbol.txt"; // Ruta del archivo de texto

            // Verificar si el archivo existe
            if (!File.Exists(filePath))
            {
                MessageBox.Show("El archivo no existe.");
                return;
            }

            // Limpiar nodos existentes en el TreeView
            treeView1.Nodes.Clear();

            // Leer todas las líneas del archivo y agregarlas al TreeView
            string[] lines = File.ReadAllLines(filePath);
            TreeNode parentNode = null;
            int lastIndent = -1;

            foreach (string line in lines)
            {
                // Contar la cantidad de tabulaciones en la línea para determinar el nivel del nodo
                int indent = 0;
                while (indent < line.Length && line[indent] == '\t')
                {
                    indent++;
                }

                // Crear el nodo con el texto de la línea (sin las tabulaciones)
                TreeNode node = new TreeNode(line.TrimStart('\t'));

                // Si el nodo tiene un nivel de indentación menor o igual que el último nodo,
                // significa que este nodo es un nuevo nodo padre
                if (indent <= lastIndent)
                {
                    parentNode.Nodes.Add(node);
                    // parentNode = null; // Reiniciar el nodo padre
                }

                // Si hay un nodo padre, agregar el nodo como hijo
                if (parentNode != null)
                {
                    parentNode.Nodes.Add(node);
                }
                else
                {
                    // Si no hay nodo padre, agregar el nodo al nivel superior
                    treeView1.Nodes.Add(node);
                }

                // Actualizar el nodo padre y el nivel de indentación para la próxima iteración
                if (indent > lastIndent)
                {
                    // El nodo actual es un hijo del último nodo, por lo que lo establecemos como nodo padre
                    parentNode = node;
                }

                lastIndent = indent;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
