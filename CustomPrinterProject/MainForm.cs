using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fargo.PrinterSDK;
using System.Management;
using System.Threading;

namespace CustomPrinterProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cbPrinter.Items.Clear();
            cbPrinter.Items.AddRange(GetPrinters());
            rtbDetails.Text = string.Empty;
        }

        private string[] GetPrinters()
        {
            ManagementObjectCollection printerCollection =
                new ManagementObjectSearcher("SELECT * FROM Win32_Printer").Get();

            var printers = new ArrayList();

            foreach (var obj in printerCollection)
            {
                var printer = (ManagementObject)obj;
                printers.Add(printer.Properties["Name"].Value.ToString());
            }

            return (string[])printers.ToArray(typeof(string));
        }

        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            var objPrinterInfo = new PrinterInfo(cbPrinter.SelectedItem.ToString());

            var printerData = new StringBuilder();
            printerData.Append("SerialNo:" + objPrinterInfo.SerialNumber + "\n");
            printerData.Append("Details:" + objPrinterInfo.ToString() + "\n");

            rtbDetails.Text = printerData.ToString();
        }

        private void btnReadMagData_Click(object sender, EventArgs e)
        {
            var objMag = new MagneticStripeCard(cbPrinter.SelectedItem.ToString());
            string track1, track2, track3;

            var movement = new Movement(cbPrinter.SelectedItem.ToString());
            movement.MoveTo(Station.Magnetic, 0);

            // Wait until the mag read is done to the firmware.  Was there an error in printer?
            if (WaitForMagReadFinish(cbPrinter.SelectedItem.ToString()))
            {
                objMag.GetMagneticData(out track1, out track2, out track3);
                rtbDetails.Text = String.Format("Track1:{0}\nTrack2:{1}\nTrack3:{2}", track1, track2, track3);
            }
        }

        /// <summary>
        /// Wait for the mag data to be finished being read from the encoder to the firmware.
        /// </summary>
        /// <returns>TRUE if mag data is ready to be read from the printer</returns>
        private bool WaitForMagReadFinish(string printerName)
        {
            // Assume that a failure will occur.
            bool result = false;
            StationStatus stationStatus;
            int m_timeout = 120000; // 2 minutes.
            bool bContinue = true;
            CurrentActivity currentActivity;
            int nTimer = Environment.TickCount;

            var printerInfo = new PrinterInfo(printerName);
            string driverName = GetDriverName(printerName);
            


            // For HDP8500 can monitor to see when the mag data has been fully read to the printers buffer.
            if (driverName.Contains("HDP8500"))
            {
                //// Because the firmware does not update the Mag StationStatus to CardNotPresent until the card is ejected we can not
                //// rely on that to tell the mag data is finished on second and later cards.
                //result = true;

                do
                {
                    currentActivity = printerInfo.CurrentActivity();

                    switch (currentActivity)
                    {
                        // Feed error?
                        case CurrentActivity.CurrentActivityException:
                            bContinue = false;
                            break;

                        default:
                            // Read to see if the station status indicates present BEFORE reading mag data.
                            stationStatus = printerInfo.StationStatus(Station.Magnetic);

                            // If card present then ok to read the data.
                            if (stationStatus == StationStatus.CardPresent)
                            {
                                result = true;
                                bContinue = false;
                            }
                            else
                            {
                                // Wait for next poll.
                                Thread.Sleep(500);
                                bContinue = true;
                            }

                            break;
                    }

                } while (((Environment.TickCount - nTimer) < m_timeout) && bContinue);

            }

            // For testing with HDP5000.
            else if (driverName.Contains("HDP5000"))
            {
                do
                {
                    currentActivity = printerInfo.CurrentActivity();

                    switch (currentActivity)
                    {
                        case CurrentActivity.CurrentActivityWaitingForEject:
                            // If the printer is holding the card then it is done.
                            // Sleep for a few seconds to make sure that the data has been read.
                            Thread.Sleep(5000);
                            result = true;
                            bContinue = false;
                            break;
                        case CurrentActivity.CurrentActivityException:
                            bContinue = false;
                            break;
                        default:
                            Thread.Sleep(1000);
                            bContinue = true;
                            break;
                    }

                } while (((Environment.TickCount - nTimer) < m_timeout) && bContinue);

            }


            // Must be Neo / Onyx model
            else
            {
                do
                {
                    currentActivity = printerInfo.CurrentActivity();

                    switch (currentActivity)
                    {
                        case CurrentActivity.CurrentActivityEncodeMagStrip:
                            // The DTC printers don't support getting the mag station status.
                            // Sleep for a few seconds to make sure that the data has been read.
                            Thread.Sleep(5000);
                            result = true;
                            bContinue = false;
                            break;
                        case CurrentActivity.CurrentActivityException:
                            bContinue = false;
                            break;
                        default:
                            Thread.Sleep(1000);
                            bContinue = true;
                            break;
                    }

                } while (((Environment.TickCount - nTimer) < m_timeout) && bContinue);
            }


            int howLong = (Environment.TickCount - nTimer);

            // Do something to breakpoint on.
            if (result == false)
            {
                if ((Environment.TickCount - nTimer) >= m_timeout)
                {
                }
            }

            return (result);
        }

        private string GetDriverName(string strPrinterName)
        {
            string retValue = string.Empty;


            //handle backslashes in network printer names
            if (strPrinterName.Contains("\\\\") == true)
                strPrinterName = strPrinterName.Replace("\\", "\\\\");

            //handle apostrophes in printer names
            if (strPrinterName.Contains("'") == true)
                strPrinterName = strPrinterName.Replace("'", "\\'");

            string searchQuery = string.Format("Select * FROM Win32_Printer Where Name = '{0}'", strPrinterName);
            ManagementObjectSearcher searchPrinters =
                new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection printerCollection = searchPrinters.Get();

            foreach (ManagementObject obj in printerCollection)
            {
                retValue = obj.Properties["DriverName"].Value.ToString();
                break;
            }

            return retValue;

        }

    }
}
