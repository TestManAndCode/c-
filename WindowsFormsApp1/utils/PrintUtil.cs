using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WindowsFormsApp1.utils
{
    class PrintUtil
    {
        public static Bitmap CreateCode(string asset)
        {
            // 1.设置条形码规格
            EncodingOptions options = new EncodingOptions();
            options.Height = 40; // 必须制定高度、宽度
            options.Width = 120;


            // 2.生成条形码图片并保存
            BarcodeWriter writer = new BarcodeWriter();
            writer.Options = options;
            writer.Format = BarcodeFormat.CODE_128;     //二维码编码
            return writer.Write(asset);     // 生成图片
        }
        public static Bitmap CreateQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8", //编码
                Width = 80,             //宽度
                Height = 80             //高度
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }
        public static void Myprinter(string printerName)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument_PrintA4Page);

            pd.DefaultPageSettings.PrinterSettings.PrinterName = printerName;       //打印机名称
                                                                                           //pd.DefaultPageSettings.Landscape = true;  //设置横向打印，不设置默认是纵向的
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.Print();
        }

        public static void printDocument_PrintA4Page(object sender, PrintPageEventArgs e)
        {
            Font titleFont = new Font("黑体", 11, System.Drawing.FontStyle.Bold);//标题字体           
            Font fntTxt = new Font("宋体", 10, System.Drawing.FontStyle.Regular);//正文文字         
            Font fntTxt1 = new Font("宋体", 8, System.Drawing.FontStyle.Regular);//正文文字           
            System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.Black);//画刷           
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);           //线条颜色         

            try
            {
                e.Graphics.DrawString("标题name", titleFont, brush, new System.Drawing.Point(20, 10));

                Point[] points111 = { new Point(20, 28), new Point(230, 28) };
                e.Graphics.DrawLines(pen, points111);

                e.Graphics.DrawString("资产编号：", fntTxt, brush, new System.Drawing.Point(20, 31));
                e.Graphics.DrawString("123456789123465", fntTxt, brush, new System.Drawing.Point(80, 31));
                e.Graphics.DrawString("资产序号：", fntTxt, brush, new System.Drawing.Point(20, 46));
                e.Graphics.DrawString("123456789131321", fntTxt, brush, new System.Drawing.Point(80, 46));

                e.Graphics.DrawString("底部name", fntTxt1, brush, new System.Drawing.Point(100, 62));

                Bitmap bitmap = CreateQRCode("此处为二维码数据");
                e.Graphics.DrawImage(bitmap, new System.Drawing.Point(240, 10));

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
