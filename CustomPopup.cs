public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void bOpenPopup_Click(object sender, EventArgs e)
		{
			CustomPopup cp = new CustomPopup()
							{
								xWidth = (int)numericUpDown1.Value, //300
								xHeight = (int)numericUpDown2.Value, //200
								Title = txtTitle.Text,
								//Create textboxes
								Textboxes = new string[] { "<txt_snir,0,0,15,15>,<txt_elgabsi,0,0,15,40>" },
							};
							
			// Create (add) ok, cancel buttons to the new form
			cp.Controls.Add(new Button()
			{
				Name = "btnOk",
				Text = "OK",
				Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
				Location = new Point(15, this.Height - 50),
				DialogResult = DialogResult.OK
			});
			cp.Controls.Add(new Button()
			{
				Name = "btnCancel",
				Text = "Cancel",
				Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
				Location = new Point(100, this.Height - 50),
				DialogResult = DialogResult.Cancel
			});
			
			// Show the new form as a dialog fomr
			if (cp.ShowDialog() == DialogResult.OK)
			{
				string ctrl_name = cp.Textboxes[0].Replace("<", "").Replace(">", "").Split(',')[0];
				this.Text = ((TextBox)cp.Controls[ctrl_name]).Text;
			}
		}
	}

public partial class CustomPopup : Form
	{
		#region Properties
		public string Title { get { return this.Text; } set { this.Text = value; } }
		public int xWidth { get { return this.Width; } set { this.Width = value; } }
		public int xHeight { get { return this.Height; } set { this.Height = value; } }
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
					this.Controls.Add(
						new TextBox()
						{
							Name = str[0],
							Width = int.Parse(str[1]) > 0 ? int.Parse(str[1]) : xWidth - int.Parse(str[3]) * 3,
							Height = int.Parse(str[2]) > 0 ? int.Parse(str[2]) : xHeight,
							Location = new Point(int.Parse(str[3]), int.Parse(str[4])),
							Anchor = int.Parse(str[1]) > 0 ? AnchorStyles.Top | AnchorStyles.Left : AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
						}
					);
				}

				this.Controls[2].Focus(); // start with 2, because 0:btnOk, 1: btnCancel
			}
		}
		#endregion

		public CustomPopup()
		{
			InitializeComponent();
		}

		private void CustomPopup_Load(object sender, EventArgs e)
		{

		}
	}
