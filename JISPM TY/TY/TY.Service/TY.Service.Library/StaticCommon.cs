using System;
using System.Net;
using System.Text;
using System.Data;
using FarPoint.Win;
using System.Drawing;
using System.Reflection;
using System.Net.Sockets;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using FarPoint.Win.Spread.CellType;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility;

namespace TY.Service.Library
{
    /// <summary>
    /// Shoveling2010.SmartClient.SystemUtility.Internal.StaticCommon
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012년 05월 24일
    /// </summary>
    internal class StaticCommon
    {
        #region Description: Common methods

        public static DateTime GetFromDateTime(object value)
        {
            try
            {
                if (value == null) value = "";
                string date = value.ToString();
                if (date.Length == 0)
                    date = "19000101000000";

                if (date.IndexOf('-') < 0 && date.IndexOf('.') < 0 &&
                    date.IndexOf('/') < 0 && date.IndexOf(':') < 0 && date.IndexOf(' ') < 0)
                {
                    switch (date.Length)
                    {
                        case 4:
                        case 6: date += "0101"; break;
                    }

                    date = date.PadRight(14, '0');
                    int year = int.Parse(date.Substring(00, 4));
                    int month = int.Parse(date.Substring(04, 2));
                    int day = int.Parse(date.Substring(06, 2));
                    int hour = int.Parse(date.Substring(08, 2));
                    int minute = int.Parse(date.Substring(10, 2));
                    int second = int.Parse(date.Substring(12, 2));

                    return new DateTime(year, month, day, hour, minute, second);
                }

                return Convert.ToDateTime(value);
            }
            catch
            {
                return Convert.ToDateTime("4444-12-31");
            }
        }

        public static string GetFromDateTimeText(string datetime, string format)
        {
            return StaticCommon.GetFromDateTimeText(
                    StaticCommon.GetFromDateTime(datetime), format);
        }

        public static string GetFromDateTimeText(DateTime datetime, string format)
        {
            string value = "";
            string check = format.ToUpper().Trim()
                .Replace("-", "")
                .Replace(":", "")
                .Replace(".", "")
                .Replace(" ", "");
            switch (check)
            {
                case "YYYY": // yyyy
                    value = datetime.ToString("yyyy");
                    break;
                case "YYYYMM": // Y
                    value = datetime.ToString("yyyyMM");
                    break;
                case "YYYYMMDDHHMM": // g
                    value = datetime.ToString("yyyyMMddHHmm");
                    break;
                case "YYYYMMDDHHMMSS": // G
                    value = datetime.ToString("yyyyMMddHHmmss");
                    break;
                case "HHMMSS": // T
                    value = datetime.ToString("HHmmss");
                    break;
                case "HHMM": // t
                    value = datetime.ToString("HHmm");
                    break;
                case "":
                    value = "";
                    break;
                default: // d
                    value = datetime.ToString("yyyyMMdd");
                    break;
            }

            return value;
        }

        public static int GetByteLength(string text)
        {
            return Encoding.Default.GetByteCount(text);
        }

        public static TimeSpan GetFromTrimSpan(string span)
        {
            return TimeSpan.Parse(span);
        }

        public static object Nvl(object value, object replace)
        {
            return (value == null) ? replace : value;
        }

        public static object DBNvl(object value1, object value2)
        {
            return (value1 != DBNull.Value) ? value1 : value2;
        }

        public static string Bool(object value)
        {
            if (value == null) return "N";

            return (value.Equals("1") ||
                    value.Equals("Y") ||
                    value.ToString().ToUpper().Equals("TRUE")) ? "Y" : "N";
        }

        public static bool GetFromBool(string value)
        {
            return Bool(value).Equals("Y") ? true : false;
        }

        public static int GetFromInt(string value)
        {
            return (value.Length == 0) ? 0 : int.Parse(value);
        }

        public static float GetFromFloat(string value)
        {
            return float.Parse(value);
        }

        public static double GetFromDouble(string value)
        {
            return double.Parse(value);
        }

        public static char GetFromChar(string value)
        {
            return char.Parse(value);
        }

        public static Color GetFromArgb(string colorText)
        {
            string[] color = colorText.Split(',');
            if (color.Length == 3)
            {
                int r = int.Parse(color[0]);
                int g = int.Parse(color[1]);
                int b = int.Parse(color[2]);

                return Color.FromArgb(r, g, b);
            }

            return Color.Empty;
        }

        public static Point GetFromPoint(string pointText)
        {
            string[] point = pointText.Split(',');
            if (point.Length == 2)
            {
                int x = int.Parse(point[0]);
                int y = int.Parse(point[1]);

                return new Point(x, y);
            }

            return new Point();
        }

        public static Size GetFromSize(string sizeText)
        {
            string[] size = sizeText.Split(',');
            if (size.Length == 2)
            {
                int width = int.Parse(size[0]);
                int height = int.Parse(size[1]);

                return new Size(width, height);
            }

            return new Size();
        }

        public static OptionDictionary CreateOptionDictionary(string option)
        {
            OptionDictionary dictionary = new OptionDictionary();
            string[] optionsArray = option.Split(';');
            for (int i = 0; i < optionsArray.Length; i++)
            {
                string[] values = optionsArray[i].Trim().Split('=');
                if (values.Length == 2)
                    if (!dictionary.ContainsKey(values[0]))
                        dictionary.Add(values[0], values[1]);
            }

            return dictionary;
        }

        public static void ControlDefaultSetting(Control control, OptionDictionary option)
        {
            if (option.Count > 0)
            {
                if (option.ContainsKey("C01")) control.BackColor = GetFromArgb(option["C01"]);
                if (option.ContainsKey("C02")) control.ForeColor = GetFromArgb(option["C02"]);
                if (option.ContainsKey("C03")) control.Location = GetFromPoint(option["C03"]);
                if (option.ContainsKey("C04")) control.Size = GetFromSize(option["C04"]);
                if (option.ContainsKey("C05")) control.Visible = GetFromBool(option["C05"]);
                if (option.ContainsKey("C06")) control.Enabled = GetFromBool(option["C06"]);
            }
        }

        public static void ControlNamingRuleCheck(string name, string prefix, ref string factoryID, ref string groupNo)
        {
            try
            {
                if (name.Length < 7 || !name.Substring(0, 3).Equals(prefix) || !name.Substring(5, 1).Equals("_"))
                    throw new Exception(String.Format("'{0}' 은 시스템의 정의된 명명규칙에 준수하지 않았습니다.", name));

                factoryID = name.Substring(6);
                groupNo = name.Substring(3, 2);
            }
            catch (Exception ex)
            {
                LocalCapturer.ExceptionCatch(ex);
            }
        }

        public static bool RequiredCheckFailMessage(string factoryName)
        {
            MessageBox.Show(String.Format("'{0}' 항목은 필수입력 항목입니다.", factoryName));
            return false;
        }

        public static FormBase GetFormBase(string assessFullName)
        {
            return (FormBase)CreateInstance(assessFullName);
        }

        public static Shoveling2010.SmartClient.SystemUtility.ControlBase GetUserControl(string assessFullName)
        {
            return (Shoveling2010.SmartClient.SystemUtility.ControlBase)CreateInstance(assessFullName);
        }

        private static object CreateInstance(string assessFullName)
        {
            if (assessFullName.Length == 0)
                throw new Exception("생성할 어셈블리 이름이 설정되지 않았습니다.");

            try
            {
                string assemblyName = AssemblyName(assessFullName);
                string assemblyDllFile = String.Format("{0}.dll", assemblyName);
                string downloadDllFile = AppDomain.CurrentDomain.BaseDirectory + assemblyDllFile;

                string deployurl =
                    MasterSystem.IsAdminClient ?
                    MasterSystem.DeployUrl : CurrentSystem.DeployUrl;

                if (Application.StartupPath.IndexOf(@"\Apps\") > -1)
                    downloadDllFile = String.Format("{0}/{1}", deployurl, assemblyDllFile);

                Assembly assembly = MasterSystem.IsSmartClient ?
                    Assembly.Load(assemblyName) :
                    Assembly.LoadFrom(downloadDllFile);
                return assembly.CreateInstance(assessFullName, true);
            }
            catch (Exception e)
            {
                LocalCapturer.ExceptionCatch(e);
                throw new Exception(String.Format("'{0}' 어셈블리를 인스턴스화 시키는 중 오류가 발생하였습니다.", assessFullName));
            }
        }

        private static string AssemblyName(string assessFullName)
        {
            string[] moduleNames = assessFullName.Split('.');
            return moduleNames[moduleNames.Length - 2];
        }

        public static string ApplicationPath(string add_path)
        {
            string fullpath = "";
            if (Application.StartupPath.IndexOf(@"\Apps\") > -1)
                fullpath = String.Format(@"{0}/{1}", MasterSystem.DeployUrl, add_path.Replace(@"\", "/"));
            else
                fullpath = String.Format(@"{0}\{1}", Application.StartupPath, add_path.Replace("/", @"\"));

            return fullpath;
        }

        public static DataTable GetDataTableSchema(params string[] parameters)
        {
            return GetDataSetSchema(parameters).Tables[0];
        }
        public static DataSet GetDataSetSchema(params string[] parameters)
        {
            DataTable source = new DataTable();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].IndexOf("$") > -1)
                {
                    string[] temp = parameters[i].Split('$');
                    source.Columns.Add("$" + temp[1]);

                    switch (temp[0].ToUpper())
                    {
                        case "BLOB":
                            source.Columns[source.Columns.Count - 1].DataType = typeof(byte[]);
                            break;
                    }
                }
                else
                    source.Columns.Add(parameters[i]);
            }

            DataSet set = new DataSet();
            set.Tables.Add(source);

            return set;
        }

        public static string GetIPAddress()
        {
            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();

            return "";
        }

        public static Point StartPopupPosition(Control control, Point location, Size size)
        {
            Control parent = control.Parent;
            while (parent != null)
            {
                if (parent.Parent == null)
                    break;

                parent = parent.Parent;
            }

            Screen currentScreen = Screen.FromControl(parent);

            int controlheight = control.Height + 1;
            int x = location.X;
            int y = location.Y + controlheight;
            int width = size.Width;
            int height = size.Height;

            if (x < currentScreen.Bounds.Left)
                x = currentScreen.Bounds.Left;

            if (x + width > currentScreen.Bounds.Right)
                x = currentScreen.Bounds.Right - width;

            if (y + height > currentScreen.Bounds.Height && height < location.Y)
                y = location.Y - height - 1;

            return new Point(x, y);
        }

        public static Point StartPopupPosition(TSpread spread, Point location, Size size)
        {
            Control parent = spread.Parent;
            while (parent != null)
            {
                if (parent.Parent == null)
                    break;

                parent = parent.Parent;
            }

            Screen currentScreen = Screen.FromControl(parent);

            int colHeadHeight = 0;
            int rowHeadWidth = 0;
            SheetView view = spread.Sheets[0];
            for (int i = 0; i < view.ColumnHeader.Rows.Count; i++)
                colHeadHeight += view.Models.ColumnHeaderRowAxis.GetSize(i);
            for (int i = 0; i < view.RowHeader.Columns.Count; i++)
                rowHeadWidth += view.Models.RowHeaderColumnAxis.GetSize(i);

            int controlheight = colHeadHeight + 1;
            int x = location.X + rowHeadWidth;
            int y = location.Y + controlheight;
            int width = size.Width;
            int height = size.Height;

            if (x < currentScreen.Bounds.Left)
                x = currentScreen.Bounds.Left;

            if (x + width > currentScreen.Bounds.Right)
                x = currentScreen.Bounds.Right - width;

            if (y + height > currentScreen.Bounds.Height && height < location.Y)
                y = location.Y - height - 1;

            return new Point(x, y);
        }

        #endregion

        #region Description: Control methods

        public static FlatStyle GetFromFlatStyle(string flatstyle)
        {
            switch (flatstyle)
            {
                case "01": return FlatStyle.Flat;
                case "02": return FlatStyle.Popup;
                case "03": return FlatStyle.Standard;
                case "04": return FlatStyle.System;
            }

            return FlatStyle.Standard;
        }

        public static ContentAlignment GetFromContentAlignment(string align)
        {
            switch (align)
            {
                case "01": return ContentAlignment.BottomCenter;
                case "02": return ContentAlignment.BottomLeft;
                case "03": return ContentAlignment.BottomRight;
                case "04": return ContentAlignment.MiddleCenter;
                case "05": return ContentAlignment.MiddleLeft;
                case "06": return ContentAlignment.MiddleRight;
                case "07": return ContentAlignment.TopCenter;
                case "08": return ContentAlignment.TopLeft;
                case "09": return ContentAlignment.TopRight;
            }

            return ContentAlignment.MiddleLeft;
        }

        public static ImeMode GetFromImeMode(string imemode)
        {
            switch (imemode)
            {
                case "01": return ImeMode.Alpha;
                case "02": return ImeMode.AlphaFull;
                case "03": return ImeMode.Close;
                case "04": return ImeMode.Disable;
                case "05": return ImeMode.Hangul;
                case "06": return ImeMode.HangulFull;
                case "07": return ImeMode.Hiragana;
                case "08": return ImeMode.Inherit;
                case "09": return ImeMode.Katakana;
                case "10": return ImeMode.KatakanaHalf;
                case "11": return ImeMode.NoControl;
                case "12": return ImeMode.Off;
                case "13": return ImeMode.On;
                case "14": return ImeMode.OnHalf;
            }

            return ImeMode.Off;
        }

        public static CharacterCasing GetFromCharacterCasing(string charText)
        {
            switch (charText)
            {
                case "01": return CharacterCasing.Lower;
                case "02": return CharacterCasing.Upper;
                case "03": return CharacterCasing.Normal;
            }

            return CharacterCasing.Normal;
        }

        public static System.Windows.Forms.HorizontalAlignment GetFromHorizontalAlignment(string align)
        {
            switch (align)
            {
                case "01": return System.Windows.Forms.HorizontalAlignment.Center;
                case "02": return System.Windows.Forms.HorizontalAlignment.Left;
                case "03": return System.Windows.Forms.HorizontalAlignment.Right;
            }

            return System.Windows.Forms.HorizontalAlignment.Left;
        }

        public static TickStyle GetFromTickStyle(string style)
        {
            switch (style)
            {
                case "01": return TickStyle.Both;
                case "02": return TickStyle.BottomRight;
                case "03": return TickStyle.None;
                case "04": return TickStyle.TopLeft;
            }

            return TickStyle.None;
        }

        public static DateTimePickerFormat GetFromDateTimePickerFormat(string format)
        {
            switch (format)
            {
                case "01": return DateTimePickerFormat.Custom;
                case "02": return DateTimePickerFormat.Long;
                case "03": return DateTimePickerFormat.Short;
                case "04": return DateTimePickerFormat.Time;
            }

            return DateTimePickerFormat.Custom;
        }

        public static LeftRightAlignment GetFromLeftRightAlignment(string align)
        {
            return (align.Equals("01")) ? LeftRightAlignment.Left : LeftRightAlignment.Right;
        }

        public static DockStyle GetFromDockStyle(string style)
        {
            switch (style)
            {
                case "01": return DockStyle.Bottom;
                case "02": return DockStyle.Fill;
                case "03": return DockStyle.Left;
                case "04": return DockStyle.None;
                case "05": return DockStyle.Right;
                case "06": return DockStyle.Top;
            }

            return DockStyle.None;
        }

        public static ComboBoxStyle GetFromComboBoxStyle(string style)
        {
            switch (style)
            {
                case "01": return ComboBoxStyle.Simple;
                case "02": return ComboBoxStyle.DropDown;
                case "03": return ComboBoxStyle.DropDownList;
            }

            return ComboBoxStyle.DropDown;
        }

        public static void SetFromColor(Color color, string colortext)
        {
            if (colortext.Length == 0) return;
            string[] rgb = colortext.Replace(" ", "").Split(',');
            if (rgb.Length != 3) return;

            int r = int.Parse(rgb[0]);
            int g = int.Parse(rgb[1]);
            int b = int.Parse(rgb[2]);

            color = Color.FromArgb(r, g, b);
        }

        public static void SetFromPoint(Point point, string pointtext)
        {
            if (pointtext.Length == 0) return;
            string[] xy = pointtext.Replace(" ", "").Split(',');
            if (xy.Length != 2) return;

            int x = int.Parse(xy[0]);
            int y = int.Parse(xy[1]);

            point = new Point(x, y);
        }

        public static void SetFromSize(Size size, string sizetext)
        {
            if (sizetext.Length == 0) return;
            string[] wh = sizetext.Replace(" ", "").Split(',');
            if (wh.Length != 2) return;

            int w = int.Parse(wh[0]);
            int h = int.Parse(wh[1]);

            size = new Size(w, h);
        }

        #endregion

        #region Description: FpSpread methods

        public static MergePolicy GetFromMergePolicy(string mergepolicy)
        {
            switch (mergepolicy)
            {
                case "01": return MergePolicy.Always;
                case "02": return MergePolicy.None;
                case "03": return MergePolicy.Restricted;
            }

            return MergePolicy.None;
        }

        public static CellVerticalAlignment GetFromCellVerticalAlignment(string align)
        {
            switch (align)
            {
                case "01": return CellVerticalAlignment.Bottom;
                case "02": return CellVerticalAlignment.Center;
                case "03": return CellVerticalAlignment.General;
                case "04": return CellVerticalAlignment.Top;
            }

            return CellVerticalAlignment.Center;
        }

        public static CellHorizontalAlignment GetFromCellHorizontalAlignment(string align)
        {
            switch (align)
            {
                case "01": return CellHorizontalAlignment.Center;
                case "02": return CellHorizontalAlignment.General;
                case "03": return CellHorizontalAlignment.Left;
                case "04": return CellHorizontalAlignment.Right;
            }

            return CellHorizontalAlignment.Left;
        }

        public static OperationMode GetFromOperationMode(string mode)
        {
            switch (mode)
            {
                case "01": return OperationMode.ExtendedSelect;
                case "02": return OperationMode.MultiSelect;
                case "03": return OperationMode.Normal;
                case "04": return OperationMode.ReadOnly;
                case "05": return OperationMode.RowMode;
                case "06": return OperationMode.SingleSelect;
            }

            return OperationMode.Normal;
        }

        public static SelectionPolicy GetFromSelectionPolicy(string policy)
        {
            switch (policy)
            {
                case "01": return SelectionPolicy.MultiRange;
                case "02": return SelectionPolicy.Range;
                case "03": return SelectionPolicy.Single;
            }

            return SelectionPolicy.Single;
        }

        public static SelectionStyles GetFromSelectionStyles(string style)
        {
            switch (style)
            {
                case "01": return SelectionStyles.Both;
                case "02": return SelectionStyles.None;
                case "03": return SelectionStyles.SelectionColors;
                case "04": return SelectionStyles.SelectionRenderer;
            }

            return SelectionStyles.None;
        }

        public static SelectionUnit GetFromSelectionUnit(string unit)
        {
            switch (unit)
            {
                case "01": return SelectionUnit.Cell;
                case "02": return SelectionUnit.Column;
                case "03": return SelectionUnit.Row;
            }

            return SelectionUnit.Row;
        }

        public static BorderStyle GetFromBorderStyle(string style)
        {
            switch (style)
            {
                case "01": return BorderStyle.Fixed3D;
                case "02": return BorderStyle.FixedSingle;
                case "03": return BorderStyle.None;
            }

            return BorderStyle.Fixed3D;
        }

        public static ClipboardOptions GetFromClipboardOptions(string options)
        {
            switch (options)
            {
                case "01": return ClipboardOptions.AllHeaders;
                case "02": return ClipboardOptions.ColumnHeaders;
                case "03": return ClipboardOptions.NoHeaders;
                case "04": return ClipboardOptions.RowHeaders;
            }

            return ClipboardOptions.AllHeaders;
        }

        public static SelectionBlockOptions GetFromSelectionBlockOptions(string options)
        {
            switch (options)
            {
                case "01": return SelectionBlockOptions.Cells;
                case "02": return SelectionBlockOptions.Columns;
                case "03": return SelectionBlockOptions.None;
                case "04": return SelectionBlockOptions.Rows;
                case "05": return SelectionBlockOptions.Sheet;
            }

            return SelectionBlockOptions.Rows;
        }

        public static ButtonDrawModes GetFromButtonDrawModes(string modes)
        {
            switch (modes)
            {
                case "01": return ButtonDrawModes.Always;
                case "02": return ButtonDrawModes.AlwaysPrimaryButton;
                case "03": return ButtonDrawModes.AlwaysSecondaryButton;
                case "04": return ButtonDrawModes.CurrentCell;
                case "05": return ButtonDrawModes.CurrentColumn;
                case "06": return ButtonDrawModes.CurrentRow;
            }

            return ButtonDrawModes.CurrentRow;
        }

        public static TextTipPolicy GetFromTextTipPolicy(string policy)
        {
            switch (policy)
            {
                case "01": return TextTipPolicy.Fixed;
                case "02": return TextTipPolicy.FixedFocusOnly;
                case "03": return TextTipPolicy.Floating;
                case "04": return TextTipPolicy.FloatingFocusOnly;
                case "05": return TextTipPolicy.Off;
            }

            return TextTipPolicy.Fixed;
        }

        public static ScrollBarPolicy GetFromScrollBarPolicy(string policy)
        {
            switch (policy)
            {
                case "01": return ScrollBarPolicy.Always;
                case "02": return ScrollBarPolicy.AsNeeded;
                case "03": return ScrollBarPolicy.Never;
            }

            return ScrollBarPolicy.AsNeeded;
        }

        public static CharacterSet GetFromCharacterSet(string set)
        {
            switch (set)
            {
                case "01": return CharacterSet.Ascii;
                case "02": return CharacterSet.Alpha;
                case "03": return CharacterSet.AlphaNumeric;
                case "04": return CharacterSet.Numeric;
            }

            return CharacterSet.Ascii;
        }

        public static CharacterCasing GetFromCellTypeCharacterCasing(string casing)
        {
            switch (casing)
            {
                case "01": return CharacterCasing.Normal;
                case "02": return CharacterCasing.Upper;
                case "03": return CharacterCasing.Lower;
            }

            return CharacterCasing.Normal;
        }

        public static TextOrientation GetFromTextOrientation(string text)
        {
            switch (text)
            {
                case "01": return TextOrientation.TextHorizontal;
                case "02": return TextOrientation.TextHorizontalFlipped;
                case "03": return TextOrientation.TextTopDown;
                case "04": return TextOrientation.TextTopDownRTL;
                case "05": return TextOrientation.TextVertical;
                case "06": return TextOrientation.TextVerticalFlipped;
                case "07": return TextOrientation.TextRotateCustom;
            }

            return TextOrientation.TextHorizontal;
        }

        public static StringTrimming GetFromStringTrimming(string trim)
        {
            switch (trim)
            {
                case "01": return StringTrimming.None;
                case "02": return StringTrimming.Character;
                case "03": return StringTrimming.Word;
                case "04": return StringTrimming.EllipsisCharacter;
                case "05": return StringTrimming.EllipsisWord;
                case "06": return StringTrimming.EllipsisPath;
            }

            return StringTrimming.None;
        }

        public static ButtonAlign GetFromButtonAlign(string align)
        {
            return (align.Equals("01")) ? ButtonAlign.Left : ButtonAlign.Right;
        }

        public static ListAlignment GetFromListAlignment(string align)
        {
            return (align.Equals("01")) ? ListAlignment.Left : ListAlignment.Right;
        }

        public static StringAlignment GetFromStringAlignment(string align)
        {
            return (align.Equals("01")) ? StringAlignment.Near : StringAlignment.Center;
        }

        public static EditorValue GetFromEditorValue(string value)
        {
            switch (value)
            {
                case "01": return EditorValue.String;
                case "02": return EditorValue.Index;
                case "03": return EditorValue.ItemData;
            }

            return EditorValue.String;
        }

        public static ComboCharacterSet GetFromComboCharacterSet(string set)
        {
            switch (set)
            {
                case "01": return ComboCharacterSet.Ascii;
                case "02": return ComboCharacterSet.Alpha;
                case "03": return ComboCharacterSet.AlphaNumeric;
                case "04": return ComboCharacterSet.Numeric;
            }

            return ComboCharacterSet.Ascii;
        }

        public static ButtonTextAlign GetFromButtonTextAlign(string align)
        {
            switch (align)
            {
                case "01": return ButtonTextAlign.TextBottomPictTop;
                case "02": return ButtonTextAlign.TextLeftPictRight;
                case "03": return ButtonTextAlign.TextRightPictLeft;
                case "04": return ButtonTextAlign.TextTopPictBottom;
            }

            return ButtonTextAlign.TextRightPictLeft;
        }

        public static DateTimeEditorValue GetFromDateTimeEditorValue(string value)
        {
            switch (value)
            {
                case "01": return DateTimeEditorValue.String;
                case "02": return DateTimeEditorValue.DateTime;
                case "03": return DateTimeEditorValue.DateSerial;
            }

            return DateTimeEditorValue.DateTime;
        }

        #endregion
    }
}
