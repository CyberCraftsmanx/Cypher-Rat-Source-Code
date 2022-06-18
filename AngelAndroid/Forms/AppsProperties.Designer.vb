<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppsProperties
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
        Me.BoxIcons = New System.Windows.Forms.PictureBox()
        Me.BOXPNL1 = New System.Windows.Forms.Panel()
        Me.LAppInstallTime = New System.Windows.Forms.Label()
        Me.LAppFlag = New System.Windows.Forms.Label()
        Me.LAppName = New System.Windows.Forms.Label()
        Me.TOpacity = New System.Windows.Forms.Timer(Me.components)
        Me.LPermissions = New System.Windows.Forms.Label()
        CType(Me.BoxIcons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BOXPNL1.SuspendLayout()
        Me.SuspendLayout()
           Me.BoxIcons.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BoxIcons.Dock = System.Windows.Forms.DockStyle.Top
        Me.BoxIcons.Location = New System.Drawing.Point(0, 0)
        Me.BoxIcons.Name = "BoxIcons"
        Me.BoxIcons.Size = New System.Drawing.Size(335, 196)
        Me.BoxIcons.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.BoxIcons.TabIndex = 0
        Me.BoxIcons.TabStop = False
           Me.BOXPNL1.AutoScroll = True
        Me.BOXPNL1.BackColor = System.Drawing.Color.Black
        Me.BOXPNL1.Controls.Add(Me.LPermissions)
        Me.BOXPNL1.Controls.Add(Me.LAppInstallTime)
        Me.BOXPNL1.Controls.Add(Me.LAppFlag)
        Me.BOXPNL1.Controls.Add(Me.LAppName)
        Me.BOXPNL1.Controls.Add(Me.BoxIcons)
        Me.BOXPNL1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BOXPNL1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.BOXPNL1.Location = New System.Drawing.Point(0, 0)
        Me.BOXPNL1.Name = "BOXPNL1"
        Me.BOXPNL1.Size = New System.Drawing.Size(335, 474)
        Me.BOXPNL1.TabIndex = 1
           Me.LAppInstallTime.Dock = System.Windows.Forms.DockStyle.Top
        Me.LAppInstallTime.Location = New System.Drawing.Point(0, 270)
        Me.LAppInstallTime.Name = "LAppInstallTime"
        Me.LAppInstallTime.Size = New System.Drawing.Size(335, 37)
        Me.LAppInstallTime.TabIndex = 3
        Me.LAppInstallTime.Text = "installTime "
        Me.LAppInstallTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
           Me.LAppFlag.Dock = System.Windows.Forms.DockStyle.Top
        Me.LAppFlag.Location = New System.Drawing.Point(0, 233)
        Me.LAppFlag.Name = "LAppFlag"
        Me.LAppFlag.Size = New System.Drawing.Size(335, 37)
        Me.LAppFlag.TabIndex = 2
        Me.LAppFlag.Text = "flag"
        Me.LAppFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
           Me.LAppName.Dock = System.Windows.Forms.DockStyle.Top
        Me.LAppName.Location = New System.Drawing.Point(0, 196)
        Me.LAppName.Name = "LAppName"
        Me.LAppName.Size = New System.Drawing.Size(335, 37)
        Me.LAppName.TabIndex = 1
        Me.LAppName.Text = "AppName"
        Me.LAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
           Me.TOpacity.Interval = 1
           Me.LPermissions.Dock = System.Windows.Forms.DockStyle.Top
        Me.LPermissions.Location = New System.Drawing.Point(0, 307)
        Me.LPermissions.Name = "LPermissions"
        Me.LPermissions.Size = New System.Drawing.Size(335, 37)
        Me.LPermissions.TabIndex = 5
        Me.LPermissions.Text = "Permissions"
        Me.LPermissions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
           Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 474)
        Me.Controls.Add(Me.BOXPNL1)
        Me.Name = "AppsProperties"
        Me.Opacity = 0R
        Me.Text = "Properties"
        CType(Me.BoxIcons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BOXPNL1.ResumeLayout(False)
        Me.ResumeLayout(False)
     End Sub
     Friend WithEvents BoxIcons As PictureBox
    Friend WithEvents BOXPNL1 As Panel
    Friend WithEvents LAppInstallTime As Label
    Friend WithEvents LAppFlag As Label
    Friend WithEvents LAppName As Label
    Friend WithEvents TOpacity As Timer
    Friend WithEvents LPermissions As Label
End Class
