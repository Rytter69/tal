using System.Linq;
using System.Windows;
using System;
using System.Net;

namespace tal
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClassIP();
            Subnetmask();
            GetNetworkIp();
            Hosts();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number = int.Parse(Tal.Text);
                string binary = Convert.ToString(number, 2);
                BiTal.Text = binary;

                string hex = Convert.ToString(number, 16).ToUpper();
                HexTal.Text = hex;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid decimal number.");
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            try
            {
                string binaryStr = BiTal.Text;
                int number = Convert.ToInt32(binaryStr, 2);
                Tal.Text = number.ToString();

                string hex = Convert.ToString(number, 16).ToUpper();
                HexTal.Text = hex.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid binary number.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string hexStr = HexTal.Text.ToString();
                int number = Convert.ToInt32(hexStr, 16);
                Tal.Text = number.ToString();

                string binary = Convert.ToString(number, 2);
                BiTal.Text = binary;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid hexadecimal number.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Tal.Text = null;
            BiTal.Text = null;
            HexTal.Text = null;
        }

        public void ClassIP()
        {
            string ipAddress = IP.Text.Trim();

            IPAddress ip = IPAddress.Parse(ipAddress);
            IPClass ipClass = GetIPClass(ip);
            Class.Text = $"{ipClass}";

        }
        public void GetNetworkIp()
        {
            string ipAddress = IP.Text.Trim();
            string subnetMask = Subnet.Text.Trim();

            if (IPAddress.TryParse(ipAddress, out IPAddress ip) && IPAddress.TryParse(subnetMask, out IPAddress subnet))
            {
                byte[] ipBytes = ip.GetAddressBytes();
                byte[] subnetBytes = subnet.GetAddressBytes();
                byte[] networkBytes = new byte[ipBytes.Length];

                for (int i = 0; i < ipBytes.Length; i++)
                {
                    networkBytes[i] = (byte)(ipBytes[i] & subnetBytes[i]);
                }

                NetworkIp.Text = new IPAddress(networkBytes).ToString();
            }
            else
            {
                MessageBox.Show("Please enter valid IP address and subnet mask.");
            }
        }
        public void Hosts()
        {
            if (Subnet.Text == "255.0.0.0")
            {
                Host.Text = "16.777.214";
            }
            else if (Subnet.Text == "255.255.0.0")
            {
                Host.Text = "65.534";
            }
            else if (Subnet.Text == "255.255.255.0")
            {
                Host.Text = "254";
            }
        }
        public void Subnetmask()
        {
            if (Class.Text == "A")
            {
                Subnet.Text = "255.0.0.0";
            }
            else if (Class.Text == "B")
            {
                Subnet.Text = "255.255.0.0";
            }
            else if (Class.Text == "C")
            {
                Subnet.Text = "255.255.255.0";
            }
        }
        private IPClass GetIPClass(IPAddress ipAddress)
        {
            byte[] bytes = ipAddress.GetAddressBytes();
            if (bytes[0] >= 0 && bytes[0] <= 127)
            {
                return IPClass.A;
            }
            else if (bytes[0] >= 128 && bytes[0] <= 191)
            {
                return IPClass.B;
            }
            else if (bytes[0] >= 192 && bytes[0] <= 223)
            {
                return IPClass.C;
            }
            else if (bytes[0] >= 224 && bytes[0] <= 239)
            {
                return IPClass.D;
            }
            else if (bytes[0] >= 240 && bytes[0] <= 255)
            {
                return IPClass.E;
            }
            else
            {
                return IPClass.Invalid;
            }
        }
        private enum IPClass
        {
            A,
            B,
            C,
            D,
            E,
            Invalid
        }
    }
}
