/*
 * Create a new popup  and define its controls dynamically 
 */

/// <summary>
/// The main form
/// </summary>
public partial class Form1 : Form
{
   public Form1()
   {
      InitializeComponent();
   }

   private void bOpenPopup_Click(object sender, EventArgs e)
   {
   	CreatePopup("<txt_snir,0,0,15,15>,<txt_elgabsi,0,0,15,40>");
   }
   
   /// <summary>
   /// Create a new popup with pre-run configured controls
   /// </summary>
   /// <param name="txtboxesToCreate">Textboxes to create in the new form.
   /// <para>for example: </para><para>   &lt;txtsnir,0,0,15,15&gt;,</para><para>  
   /// &lt;txt2,0,0,15,40&gt;,</para><para>   &lt;txt3,0,0,15,65&gt;,</para>
   /// </param>
   private void CreatePopup(string textboxes_to_create)
   {
      CustomPopup _custompopup = new CustomPopup()
      {
         xWidth = 300, //(int)numericUpDown1.Value
         xHeight = 200, //(int)numericUpDown2.Value
         Title = "my popop title",
         
         //Create textboxes
         Textboxes = new string[] { textboxes_to_create },
      };
							
      // Create (add) ok, cancel buttons to the new form
      _custompopup.Controls.Add(new Button()
      {
         Name = "btnOk",
         Text = "OK",
         Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
         Location = new Point(15, this.Height - 50),
         DialogResult = DialogResult.OK
      });
      _custompopup.Controls.Add(new Button()
      {
         Name = "btnCancel",
         Text = "Cancel",
         Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
         Location = new Point(100, this.Height - 50),
         DialogResult = DialogResult.Cancel
      });
			
      // Show the new form as a dialog fomr
      if (_custompopup.ShowDialog() == DialogResult.OK)
      {
         //Get the text from the text box to this form's title (this.Text)
         string ctrl_name = _custompopup.Textboxes[0].Replace("<", "").Replace(">", "").Split(',')[0];
         this.Text = ((TextBox)_custompopup.Controls[ctrl_name]).Text;
      }
   }
}


/// <summary>
/// The new form to configure and popup
/// </summary>
public partial class CustomPopup : Form
{
   public CustomPopup()
   {
      InitializeComponent();
   }
   
   #region Properties
   /// <summary> The new form's (popup) title </summary>
   public string Title { get { return this.Text; } set { this.Text = value; } }
   /// <summary> The new form's (popup) width </summary>
   public int xWidth { get { return this.Width; } set { this.Width = value; } }
   /// <summary> The new form's (popup) height </summary>
   public int xHeight { get { return this.Height; } set { this.Height = value; } }
   
   /// <summary>
   /// The new form's (popup) custom new textboxes. 
   /// I can add any controls i'd like in the same way.
   /// </summary>
   public string[] Textboxes
   {
      get
      {
         List<string> ctrls_tbs = new List<string>();
         foreach (Control _ctrl_tb in this.Controls)
            if (_ctrl_tb is TextBox)
               ctrls_tbs.Add("<" + _ctrl_tb.Name + "," + _ctrl_tb.Width.ToString() + "," + _ctrl_tb.Height.ToString() + "," + _ctrl_tb.Location.X.ToString() + "," + Location.Y.ToString() + ">");
         return ctrls_tbs.ToArray();
      }
      set
      {
         // set new textboxes from string: <"Name",intWdt,intHgt,intX,intY>
         string[] _txtbs = value[0].Split(new string[]{">,<"},StringSplitOptions.RemoveEmptyEntries);
         foreach (string txtb in _txtbs)
         {
            string[] str = txtb.Trim().Replace("<", "").Replace(">", "").Split(',');
            this.Controls.Add(new TextBox()
            {
               Name = str[0],
               Width = int.Parse(str[1]) > 0 ? int.Parse(str[1]) : xWidth - int.Parse(str[3]) * 3,
               Height = int.Parse(str[2]) > 0 ? int.Parse(str[2]) : xHeight,
               Location = new Point(int.Parse(str[3]), int.Parse(str[4])),
               Anchor = int.Parse(str[1]) > 0 ? AnchorStyles.Top | AnchorStyles.Left : AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            });
         }
         this.Controls[2].Focus(); // start with 2, because 0:btnOk, 1: btnCancel
      }
   }
   #endregion
}
