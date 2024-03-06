using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


using System.Data;

namespace TY.ER.US00
{
    /// <summary>
    /// Summary description for TYUSPR002R1.
    /// </summary>
    public partial class TYUSPR002R1 : GrapeCity.ActiveReports.SectionReport
    {
        private DataTable fgkdt;

        public TYUSPR002R1(DataTable gkdt)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            fgkdt = gkdt;
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //this.groupFooter1.NewPage = NewPage.After;                       

        }

        private void TYUSPR002R1_DataInitialize(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.DataSource;
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {

            //if (fgkdt.Rows.Count > 0)
            //{
            //    string sGKGubn = string.Empty;

            //    double dDHQTYTotal = 0;
            //    double dJEGOQTYTotal = 0;
            //    double dCarCntTotal = 0;

            //    for (int i = 0; i < fgkdt.Rows.Count; i++)
            //    {
            //        sGKGubn = fgkdt.Rows[i]["GKGUBN"].ToString();

            //        dDHQTYTotal += Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString());
            //        dJEGOQTYTotal += Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString());
            //        dCarCntTotal += Convert.ToDouble(fgkdt.Rows[i]["CARCNT"].ToString());

            //        switch (sGKGubn)
            //        {
            //            case "1": //옥수수
            //                txtDHCHULQTY1.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
            //                txtJEGOQTY1.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
            //                break;
            //            case "2": //소맥
            //                txtDHCHULQTY2.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
            //                txtJEGOQTY2.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
            //                break;
            //            case "3": //보리
            //                txtDHCHULQTY3.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
            //                txtJEGOQTY3.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
            //                break;
            //            case "4": //호밀
            //                txtDHCHULQTY4.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
            //                txtJEGOQTY4.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
            //                break;
            //            case "5": //소맥피
            //                txtDHCHULQTY5.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
            //                txtJEGOQTY5.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
            //                break;
            //            case "6": //우드펠렛
            //                txtDHCHULQTY6.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
            //                txtJEGOQTY6.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
            //                break;
            //            default:
            //                break;
            //        }
            //    }

            //    txtDHCHULQTYHAP.Text = String.Format("{0:#,##0.000}", dDHQTYTotal);
            //    txtJEGOQTYHAP.Text = String.Format("{0:#,##0.000}", dJEGOQTYTotal);
            //    txtCARCNT.Text = String.Format("{0:#,##0}", dCarCntTotal);  
            //}
        }

        private void groupHeader1_Format(object sender, EventArgs e)
        {
            if (fgkdt.Rows.Count > 0)
            {
                string sGKGubn = string.Empty;

                double dDHQTYTotal = 0;
                double dJEGOQTYTotal = 0;
                double dCarCntTotal = 0;

                for (int i = 0; i < fgkdt.Rows.Count; i++)
                {
                    sGKGubn = fgkdt.Rows[i]["GKGUBN"].ToString();

                    dDHQTYTotal += Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString());
                    dJEGOQTYTotal += Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString());
                    dCarCntTotal += Convert.ToDouble(fgkdt.Rows[i]["CARCNT"].ToString());

                    switch (sGKGubn)
                    {
                        case "1": //옥수수
                            txtDHCHULQTY1.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
                            txtJEGOQTY1.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
                            break;
                        case "2": //소맥
                            txtDHCHULQTY2.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
                            txtJEGOQTY2.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
                            break;
                        case "3": //보리
                            txtDHCHULQTY3.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
                            txtJEGOQTY3.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
                            break;
                        case "4": //호밀
                            if (Convert.ToDouble(fgkdt.Rows[i]["PSHANGCHA"].ToString()) >= 202101)
                            {
                                label21.Text = "제  분   밀";
                            }
                            else
                            {
                                label21.Text = "호       밀";
                            }
                            txtDHCHULQTY4.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
                            txtJEGOQTY4.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
                            break;
                        case "5": //소맥피
                            txtDHCHULQTY5.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
                            txtJEGOQTY5.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
                            break;
                        case "6": //우드펠렛
                            txtDHCHULQTY6.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["DHQTY"].ToString()));
                            txtJEGOQTY6.Text = String.Format("{0:#,##0.000}", Convert.ToDouble(fgkdt.Rows[i]["JEGOQTY"].ToString()));
                            break;
                        default:
                            break;
                    }
                }

                txtDHCHULQTYHAP.Text = String.Format("{0:#,##0.000}", dDHQTYTotal);
                txtJEGOQTYHAP.Text = String.Format("{0:#,##0.000}", dJEGOQTYTotal);
                txtCARCNT.Text = String.Format("{0:#,##0}", dCarCntTotal);
            }
        }
    }
}