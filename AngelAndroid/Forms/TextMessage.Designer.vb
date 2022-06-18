<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TextMessage
    Inherits System.Windows.Forms.Form
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
      Private components As System.ComponentModel.IContainer
        <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TextMsg = New System.Windows.Forms.RichTextBox()
        Me.TOpacity = New System.Windows.Forms.Timer(Me.components)
        Me.CMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CMenu.SuspendLayout()
        Me.SuspendLayout()
           Me.TextMsg.BackColor = System.Drawing.Color.Black
        Me.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.TextMsg.Location = New System.Drawing.Point(0, 0)
        Me.TextMsg.Name = "TextMsg"
        Me.TextMsg.Size = New System.Drawing.Size(414, 241)
        Me.TextMsg.TabIndex = 0
        Me.TextMsg.Text = ""
        Me.TextMsg.WordWrap = False
           Me.TOpacity.Interval = 1
           Me.CMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem})
        Me.CMenu.Name = "CMenu"
        Me.CMenu.ShowImageMargin = False
        Me.CMenu.Size = New System.Drawing.Size(78, 70)
           Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
           Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
           Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
           Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 241)
        Me.Controls.Add(Me.TextMsg)
        Me.Name = "TextMessage"
        Me.Opacity = 0R
        Me.Text = "TextMessage"
        Me.CMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
     End Sub
     Friend WithEvents TextMsg As RichTextBox
    Friend WithEvents TOpacity As Timer
    Friend WithEvents CMenu As ContextMenuStrip
    Friend WithEvents CutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As ToolStripMenuItem
End Class
