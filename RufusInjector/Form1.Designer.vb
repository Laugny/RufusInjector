<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Button1 = New Siticone.UI.WinForms.SiticoneButton()
        Me.Button2 = New Siticone.UI.WinForms.SiticoneButton()
        Me.Button3 = New Siticone.UI.WinForms.SiticoneButton()
        Me.Button4 = New Siticone.UI.WinForms.SiticoneButton()
        Me.Button5 = New Siticone.UI.WinForms.SiticoneButton()
        Me.DLLs = New System.Windows.Forms.ListBox()
        Me.RadioButton1 = New Siticone.UI.WinForms.SiticoneRadioButton()
        Me.RadioButton2 = New Siticone.UI.WinForms.SiticoneRadioButton()
        Me.CheckBox1 = New Siticone.UI.WinForms.SiticoneCheckBox()
        Me.TextBox1 = New Siticone.UI.WinForms.SiticoneTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.CheckedState.Parent = Me.Button1
        Me.Button1.CustomImages.Parent = Me.Button1
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.HoveredState.Parent = Me.Button1
        Me.Button1.Location = New System.Drawing.Point(188, 120)
        Me.Button1.Name = "Button1"
        Me.Button1.ShadowDecoration.Parent = Me.Button1
        Me.Button1.Size = New System.Drawing.Size(75, 30)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Browse"
        '
        'Button2
        '
        Me.Button2.CheckedState.Parent = Me.Button2
        Me.Button2.CustomImages.Parent = Me.Button2
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.HoveredState.Parent = Me.Button2
        Me.Button2.Location = New System.Drawing.Point(188, 159)
        Me.Button2.Name = "Button2"
        Me.Button2.ShadowDecoration.Parent = Me.Button2
        Me.Button2.Size = New System.Drawing.Size(75, 30)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Remove"
        '
        'Button3
        '
        Me.Button3.CheckedState.Parent = Me.Button3
        Me.Button3.CustomImages.Parent = Me.Button3
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.HoveredState.Parent = Me.Button3
        Me.Button3.Location = New System.Drawing.Point(188, 198)
        Me.Button3.Name = "Button3"
        Me.Button3.ShadowDecoration.Parent = Me.Button3
        Me.Button3.Size = New System.Drawing.Size(75, 30)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Clear List"
        '
        'Button4
        '
        Me.Button4.CheckedState.Parent = Me.Button4
        Me.Button4.CustomImages.Parent = Me.Button4
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.HoveredState.Parent = Me.Button4
        Me.Button4.Location = New System.Drawing.Point(156, 297)
        Me.Button4.Name = "Button4"
        Me.Button4.ShadowDecoration.Parent = Me.Button4
        Me.Button4.Size = New System.Drawing.Size(107, 47)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "Inject"
        '
        'Button5
        '
        Me.Button5.CheckedState.Parent = Me.Button5
        Me.Button5.CustomImages.Parent = Me.Button5
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.HoveredState.Parent = Me.Button5
        Me.Button5.Location = New System.Drawing.Point(25, 297)
        Me.Button5.Name = "Button5"
        Me.Button5.ShadowDecoration.Parent = Me.Button5
        Me.Button5.Size = New System.Drawing.Size(107, 47)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Quit"
        '
        'DLLs
        '
        Me.DLLs.FormattingEnabled = True
        Me.DLLs.Location = New System.Drawing.Point(25, 120)
        Me.DLLs.Name = "DLLs"
        Me.DLLs.Size = New System.Drawing.Size(151, 108)
        Me.DLLs.TabIndex = 5
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioButton1.CheckedState.BorderThickness = 0
        Me.RadioButton1.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioButton1.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RadioButton1.Location = New System.Drawing.Point(25, 234)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(60, 17)
        Me.RadioButton1.TabIndex = 6
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Manual"
        Me.RadioButton1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.RadioButton1.UncheckedState.BorderThickness = 2
        Me.RadioButton1.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RadioButton1.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioButton2.CheckedState.BorderThickness = 0
        Me.RadioButton2.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioButton2.CheckedState.InnerColor = System.Drawing.Color.White
        Me.RadioButton2.Location = New System.Drawing.Point(25, 257)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(72, 17)
        Me.RadioButton2.TabIndex = 7
        Me.RadioButton2.Text = "Automatic"
        Me.RadioButton2.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.RadioButton2.UncheckedState.BorderThickness = 2
        Me.RadioButton2.UncheckedState.FillColor = System.Drawing.Color.Transparent
        Me.RadioButton2.UncheckedState.InnerColor = System.Drawing.Color.Transparent
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CheckBox1.CheckedState.BorderRadius = 2
        Me.CheckBox1.CheckedState.BorderThickness = 0
        Me.CheckBox1.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CheckBox1.Location = New System.Drawing.Point(25, 350)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(119, 17)
        Me.CheckBox1.TabIndex = 8
        Me.CheckBox1.Text = "Close after Injection"
        Me.CheckBox1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.CheckBox1.UncheckedState.BorderRadius = 2
        Me.CheckBox1.UncheckedState.BorderThickness = 0
        Me.CheckBox1.UncheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox1.DefaultText = ""
        Me.TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBox1.DisabledState.Parent = Me.TextBox1
        Me.TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBox1.FocusedState.Parent = Me.TextBox1
        Me.TextBox1.HoveredState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBox1.HoveredState.Parent = Me.TextBox1
        Me.TextBox1.Location = New System.Drawing.Point(25, 93)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox1.PlaceholderText = ""
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.ShadowDecoration.Parent = Me.TextBox1
        Me.TextBox1.Size = New System.Drawing.Size(151, 21)
        Me.TextBox1.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Label1"
        '
        'Timer1
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 25)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Made by"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LinkLabel1.Location = New System.Drawing.Point(104, 27)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(77, 25)
        Me.LinkLabel1.TabIndex = 12
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Laugny"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 25)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "_____"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(286, 380)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.DLLs)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Rufus Injection V1.0"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Siticone.UI.WinForms.SiticoneButton
    Friend WithEvents Button2 As Siticone.UI.WinForms.SiticoneButton
    Friend WithEvents Button3 As Siticone.UI.WinForms.SiticoneButton
    Friend WithEvents Button4 As Siticone.UI.WinForms.SiticoneButton
    Friend WithEvents Button5 As Siticone.UI.WinForms.SiticoneButton
    Friend WithEvents DLLs As ListBox
    Friend WithEvents RadioButton1 As Siticone.UI.WinForms.SiticoneRadioButton
    Friend WithEvents RadioButton2 As Siticone.UI.WinForms.SiticoneRadioButton
    Friend WithEvents CheckBox1 As Siticone.UI.WinForms.SiticoneCheckBox
    Friend WithEvents TextBox1 As Siticone.UI.WinForms.SiticoneTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label2 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label3 As Label
End Class
