<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddNumber
    Inherits System.Windows.Forms.Form
      <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
      Private components As System.ComponentModel.IContainer
        <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.InputText1 = New System.Windows.Forms.TextBox()
        Me.L1 = New System.Windows.Forms.Label()
        Me.b_ok = New System.Windows.Forms.Button()
        Me.InputText0 = New System.Windows.Forms.TextBox()
        Me.L0 = New System.Windows.Forms.Label()
        Me.TOpacity = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()



        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.InputText1)
        Me.Panel1.Controls.Add(Me.L1)
        Me.Panel1.Controls.Add(Me.b_ok)
        Me.Panel1.Controls.Add(Me.InputText0)
        Me.Panel1.Controls.Add(Me.L0)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(443, 196)
        Me.Panel1.TabIndex = 1



        Me.InputText1.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.InputText1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.InputText1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.InputText1.Location = New System.Drawing.Point(12, 122)
        Me.InputText1.Name = "InputText1"
        Me.InputText1.Size = New System.Drawing.Size(419, 13)
        Me.InputText1.TabIndex = 5



        Me.L1.AutoSize = True
        Me.L1.Location = New System.Drawing.Point(12, 89)
        Me.L1.Name = "L1"
        Me.L1.Size = New System.Drawing.Size(23, 13)
        Me.L1.TabIndex = 4
        Me.L1.Text = "null"



        Me.b_ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.b_ok.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.b_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.b_ok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.b_ok.Location = New System.Drawing.Point(12, 152)
        Me.b_ok.Name = "b_ok"
        Me.b_ok.Size = New System.Drawing.Size(419, 32)
        Me.b_ok.TabIndex = 3
        Me.b_ok.Text = "Add New Number"
        Me.b_ok.UseVisualStyleBackColor = False



        Me.InputText0.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.InputText0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.InputText0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.InputText0.Location = New System.Drawing.Point(12, 56)
        Me.InputText0.Name = "InputText0"
        Me.InputText0.Size = New System.Drawing.Size(419, 13)
        Me.InputText0.TabIndex = 2



        Me.L0.AutoSize = True
        Me.L0.Location = New System.Drawing.Point(12, 23)
        Me.L0.Name = "L0"
        Me.L0.Size = New System.Drawing.Size(23, 13)
        Me.L0.TabIndex = 1
        Me.L0.Text = "null"



        Me.TOpacity.Interval = 1



        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 196)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddNumber"
        Me.Opacity = 0R
        Me.ShowInTaskbar = False
        Me.Text = "Add Number"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents InputText1 As TextBox
    Friend WithEvents L1 As Label
    Friend WithEvents b_ok As Button
    Friend WithEvents InputText0 As TextBox
    Friend WithEvents L0 As Label
    Friend WithEvents TOpacity As Timer
End Class
