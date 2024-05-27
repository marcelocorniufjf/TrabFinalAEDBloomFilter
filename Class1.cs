using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BloomFilterWindowsForms
{
    public class BloomFilter
    {
        private readonly int size;
        private readonly bool[] bitArray;
        private readonly int hashCount;
        private readonly PictureBox pictureBox;
        private string operationDetails;
        private List<int> insertedValues;

        public BloomFilter(int size, int hashCount, PictureBox pictureBox)
        {
            this.size = size;
            this.hashCount = hashCount;
            this.bitArray = new bool[size];
            this.pictureBox = pictureBox;
            this.operationDetails = "";
            this.insertedValues = new List<int>();
            DrawFilter();
        }

        private int[] GetHashes(int key)
        {
            var hashes = new int[hashCount];
            var random = new Random(key);
            for (int i = 0; i < hashCount; i++)
            {
                hashes[i] = random.Next(size);
            }
            return hashes;
        }

        public void Insert(int key)
        {
            if (insertedValues.Contains(key))
            {
                operationDetails = $"Valor duplicado {key} não inserido.";
                DrawFilter();
                return;
            }

            var hashes = GetHashes(key);
            foreach (var hash in hashes)
            {
                bitArray[hash] = true;
            }
            insertedValues.Add(key);
            operationDetails = $"Inserindo {key} com hashes {string.Join(", ", hashes)}";
            DrawFilter();
        }

        public bool Lookup(int key)
        {
            var hashes = GetHashes(key);
            bool isPresent = true;
            foreach (var hash in hashes)
            {
                if (!bitArray[hash])
                {
                    isPresent = false;
                    break;
                }
            }

            if (isPresent)
            {
                if (insertedValues.Contains(key))
                {
                    operationDetails = $"Encontrado {key} com hashes {string.Join(", ", hashes)}";
                    DrawFilter(hashes, Color.Green);
                }
                else
                {
                    operationDetails = $"Falso positivo {key} com hashes {string.Join(", ", hashes)}";
                    DrawFilter(hashes, Color.Red);
                }
            }
            else
            {
                operationDetails = $"Não encontrado {key} com hashes {string.Join(", ", hashes)}";
                DrawFilter(hashes, Color.Red);
            }
            return isPresent;
        }

        private void DrawFilter(int[] highlightHashes = null, Color? highlightColor = null)
        {
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                for (int i = 0; i < size; i++)
                {
                    Color color = bitArray[i] ? Color.Blue : Color.White;
                    if (highlightHashes != null && Array.Exists(highlightHashes, h => h == i))
                    {
                        color = highlightColor ?? Color.White;
                    }
                    DrawBit(g, i, color);
                }

                DrawFilterParameters(g);
                DrawOperationDetails(g);
                DrawInsertedValues(g);
            }
            pictureBox.Image = bitmap;
        }

        private void DrawBit(Graphics g, int index, Color color)
        {
            int bitWidth = pictureBox.Width / size;
            int x = index * bitWidth;
            g.FillRectangle(new SolidBrush(color), x, 20, bitWidth, pictureBox.Height / 2);
            g.DrawRectangle(Pens.Black, x, 20, bitWidth, pictureBox.Height / 2);

            // Desenhar o índice do bit
            g.DrawString(index.ToString(), SystemFonts.DefaultFont, Brushes.Black, x, 0);
        }

        private void DrawFilterParameters(Graphics g)
        {
            string parameters = $"Tamanho do filtro: {size}, Número de funções hash: {hashCount}";
            g.DrawString(parameters, SystemFonts.DefaultFont, Brushes.Black, 10, pictureBox.Height - 60);
        }

        private void DrawOperationDetails(Graphics g)
        {
            g.DrawString(operationDetails, SystemFonts.DefaultFont, Brushes.Black, 10, pictureBox.Height - 40);
        }

        private void DrawInsertedValues(Graphics g)
        {
            string values = $"Chaves inseridas: {string.Join(", ", insertedValues)}";
            g.DrawString(values, SystemFonts.DefaultFont, Brushes.Magenta, 10, pictureBox.Height - 20);
        }
    }

}
