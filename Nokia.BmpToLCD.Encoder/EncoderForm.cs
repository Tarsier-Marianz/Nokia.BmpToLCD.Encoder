using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nokia.BmpToLCD.Encoder {
    public partial class EncoderForm : Form {
        private const int asciiDiff = 48;
        private Bitmap _inputImage;
        private Bitmap _origImage = new Bitmap(168, 96);
        private Bitmap _resizedImage = new Bitmap(84, 48);
        private int _threshold = 128;
        private StringBuilder _outputCode = new StringBuilder();
        public EncoderForm() {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            using(OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = "Load Image";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|Gif files (*.gif)|*.gif|All valid files (*.bmp/*.jpg/*.gif)|*.bmp; *.jpg; *.gif";
                openFileDialog.FilterIndex = 4;
                openFileDialog.RestoreDirectory = true;
                if(DialogResult.OK == openFileDialog.ShowDialog()) {
                    _inputImage = (Bitmap)Image.FromFile(openFileDialog.FileName, false);
                    for(int i = 0; i < 84; i++) {
                        for(int j = 0; j < 48; j++) {
                            _resizedImage.SetPixel(i, j, _inputImage.GetPixel(i * _inputImage.Width / 84, j * _inputImage.Height / 48));
                        }
                    }
                    for(int i = 0; i < 168; i++) {
                        for(int j = 0; j < 96; j++) {
                            _origImage.SetPixel(i, j, _inputImage.GetPixel(i * _inputImage.Width / 168, j * _inputImage.Height / 96));
                        }
                    }
                    this.pictureBoxOrig.Image = _origImage;
                    this.pictureBoxOrig.Invalidate();
                    base.Invalidate();
                }
            }
        }

        private void btnEncode_Click(object sender, EventArgs e) {
            int num = 0;
            Bitmap bitmap = new Bitmap(84, 48);
            Color lightGray = Color.LightGray;
            Color black = Color.Black;
            _outputCode = new StringBuilder();
            _outputCode.AppendLine("const unsigned char image[504] = {");
            for(int i = 0; i < 6; i++) {
                for(int j = 0; j < 84; j++) {
                    string text = "";
                    for(int k = 7; k >= 0; k--) {
                        Color pixel = _resizedImage.GetPixel(j, k + i * 8);
                        int num2 = (int)((pixel.R * 30 + pixel.G * 59 + pixel.B * 11) / 100);
                        if(num2 < this._threshold) {
                            text += "1";
                            bitmap.SetPixel(j, k + i * 8, black);
                        } else {
                            text += "0";
                            bitmap.SetPixel(j, k + i * 8, lightGray);
                        }
                    }
                    _outputCode.Append(text.Bin2Hex());
                    if(num < 503) {
                        _outputCode.Append(", ");
                    } else {
                        _outputCode.AppendLine(" };");
                    }
                    num++;
                }
            }

            txtOutput.Text = _outputCode.ToString();
            pictureBoxPreview.Image = bitmap;
            pictureBoxPreview.Invalidate();
        }
    }
}