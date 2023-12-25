<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class zgaa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(zgaa))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.IconButton1 = New FontAwesome.Sharp.IconButton()
        Me.IconButton2 = New FontAwesome.Sharp.IconButton()
        Me.PicBarCode = New System.Windows.Forms.PictureBox()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.prnt_prev = New System.Windows.Forms.PrintPreviewDialog()
        Me.prnt_doc = New System.Drawing.Printing.PrintDocument()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.butPageSetup = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PicBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(50, 78)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(172, 20)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(50, 133)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(253, 20)
        Me.TextBox2.TabIndex = 1
        '
        'IconButton1
        '
        Me.IconButton1.IconChar = FontAwesome.Sharp.IconChar.None
        Me.IconButton1.IconColor = System.Drawing.Color.Black
        Me.IconButton1.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton1.Location = New System.Drawing.Point(342, 34)
        Me.IconButton1.Name = "IconButton1"
        Me.IconButton1.Size = New System.Drawing.Size(75, 23)
        Me.IconButton1.TabIndex = 2
        Me.IconButton1.Text = "IconButton1"
        Me.IconButton1.UseVisualStyleBackColor = True
        '
        'IconButton2
        '
        Me.IconButton2.IconChar = FontAwesome.Sharp.IconChar.None
        Me.IconButton2.IconColor = System.Drawing.Color.Black
        Me.IconButton2.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton2.Location = New System.Drawing.Point(544, 200)
        Me.IconButton2.Name = "IconButton2"
        Me.IconButton2.Size = New System.Drawing.Size(75, 23)
        Me.IconButton2.TabIndex = 3
        Me.IconButton2.Text = "IconButton2"
        Me.IconButton2.UseVisualStyleBackColor = True
        '
        'PicBarCode
        '
        Me.PicBarCode.BackColor = System.Drawing.Color.White
        Me.PicBarCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicBarCode.Location = New System.Drawing.Point(177, 194)
        Me.PicBarCode.Name = "PicBarCode"
        Me.PicBarCode.Size = New System.Drawing.Size(294, 111)
        Me.PicBarCode.TabIndex = 29
        Me.PicBarCode.TabStop = False
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'prnt_prev
        '
        Me.prnt_prev.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.prnt_prev.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.prnt_prev.ClientSize = New System.Drawing.Size(400, 300)
        Me.prnt_prev.Enabled = True
        Me.prnt_prev.Icon = CType(resources.GetObject("prnt_prev.Icon"), System.Drawing.Icon)
        Me.prnt_prev.Name = "prnt_prev"
        Me.prnt_prev.Visible = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(494, 254)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(294, 40)
        Me.Button4.TabIndex = 108
        Me.Button4.Text = "Type the product name on the barcode"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(50, 175)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 48)
        Me.Button1.TabIndex = 109
        Me.Button1.Text = "Preview"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'butPageSetup
        '
        Me.butPageSetup.Location = New System.Drawing.Point(35, 12)
        Me.butPageSetup.Name = "butPageSetup"
        Me.butPageSetup.Size = New System.Drawing.Size(120, 56)
        Me.butPageSetup.TabIndex = 107
        Me.butPageSetup.Text = "Page setup from system"
        Me.butPageSetup.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(52, 229)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(103, 87)
        Me.Button5.TabIndex = 110
        Me.Button5.Text = "Exit"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(342, 63)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(218, 62)
        Me.PictureBox1.TabIndex = 111
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(570, 63)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(218, 62)
        Me.PictureBox2.TabIndex = 112
        Me.PictureBox2.TabStop = False
        '
        'zgaa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.butPageSetup)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.PicBarCode)
        Me.Controls.Add(Me.IconButton2)
        Me.Controls.Add(Me.IconButton1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "zgaa"
        Me.Text = "zgaa"
        CType(Me.PicBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents IconButton1 As FontAwesome.Sharp.IconButton
    Friend WithEvents IconButton2 As FontAwesome.Sharp.IconButton
    Friend WithEvents PicBarCode As PictureBox
    Friend WithEvents PageSetupDialog1 As PageSetupDialog
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents prnt_prev As PrintPreviewDialog
    Friend WithEvents prnt_doc As Printing.PrintDocument
    Friend WithEvents Button4 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents butPageSetup As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
End Class
