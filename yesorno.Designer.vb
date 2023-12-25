<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class yesorno
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(yesorno))
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.IconButton4 = New FontAwesome.Sharp.IconButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.IconButton1 = New FontAwesome.Sharp.IconButton()
        Me.IconButton2 = New FontAwesome.Sharp.IconButton()
        Me.IconButton3 = New FontAwesome.Sharp.IconButton()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(4, 196)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(492, 4)
        Me.Panel4.TabIndex = 301
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(496, 36)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(4, 164)
        Me.Panel3.TabIndex = 300
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 36)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(4, 164)
        Me.Panel2.TabIndex = 299
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.Location = New System.Drawing.Point(84, 49)
        Me.Label14.MaximumSize = New System.Drawing.Size(370, 200)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(370, 55)
        Me.Label14.TabIndex = 298
        Me.Label14.Text = "Désignation :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 36)
        Me.Panel1.TabIndex = 295
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.MaximumSize = New System.Drawing.Size(370, 200)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(370, 70)
        Me.Label1.TabIndex = 299
        Me.Label1.Text = "UP Gestion Message"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.IconButton4)
        Me.Panel5.Location = New System.Drawing.Point(15, 133)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(469, 58)
        Me.Panel5.TabIndex = 305
        Me.Panel5.Visible = False
        '
        'IconButton4
        '
        Me.IconButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.IconButton4.FlatAppearance.BorderSize = 0
        Me.IconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton4.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IconButton4.ForeColor = System.Drawing.Color.White
        Me.IconButton4.IconChar = FontAwesome.Sharp.IconChar.None
        Me.IconButton4.IconColor = System.Drawing.Color.White
        Me.IconButton4.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton4.IconSize = 25
        Me.IconButton4.Location = New System.Drawing.Point(361, 20)
        Me.IconButton4.Name = "IconButton4"
        Me.IconButton4.Size = New System.Drawing.Size(101, 34)
        Me.IconButton4.TabIndex = 297
        Me.IconButton4.Text = "Ok"
        Me.IconButton4.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(10, 49)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(68, 55)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 297
        Me.PictureBox1.TabStop = False
        '
        'IconButton1
        '
        Me.IconButton1.BackColor = System.Drawing.Color.DimGray
        Me.IconButton1.FlatAppearance.BorderSize = 0
        Me.IconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IconButton1.ForeColor = System.Drawing.Color.White
        Me.IconButton1.IconChar = FontAwesome.Sharp.IconChar.None
        Me.IconButton1.IconColor = System.Drawing.Color.White
        Me.IconButton1.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton1.IconSize = 25
        Me.IconButton1.Location = New System.Drawing.Point(137, 153)
        Me.IconButton1.Name = "IconButton1"
        Me.IconButton1.Size = New System.Drawing.Size(101, 34)
        Me.IconButton1.TabIndex = 306
        Me.IconButton1.Text = "Annuler"
        Me.IconButton1.UseVisualStyleBackColor = False
        '
        'IconButton2
        '
        Me.IconButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.IconButton2.FlatAppearance.BorderSize = 0
        Me.IconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IconButton2.ForeColor = System.Drawing.Color.White
        Me.IconButton2.IconChar = FontAwesome.Sharp.IconChar.None
        Me.IconButton2.IconColor = System.Drawing.Color.White
        Me.IconButton2.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton2.IconSize = 25
        Me.IconButton2.Location = New System.Drawing.Point(376, 153)
        Me.IconButton2.Name = "IconButton2"
        Me.IconButton2.Size = New System.Drawing.Size(101, 34)
        Me.IconButton2.TabIndex = 307
        Me.IconButton2.Text = "Non"
        Me.IconButton2.UseVisualStyleBackColor = False
        '
        'IconButton3
        '
        Me.IconButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.IconButton3.FlatAppearance.BorderSize = 0
        Me.IconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton3.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IconButton3.ForeColor = System.Drawing.Color.White
        Me.IconButton3.IconChar = FontAwesome.Sharp.IconChar.None
        Me.IconButton3.IconColor = System.Drawing.Color.White
        Me.IconButton3.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton3.IconSize = 25
        Me.IconButton3.Location = New System.Drawing.Point(256, 153)
        Me.IconButton3.Name = "IconButton3"
        Me.IconButton3.Size = New System.Drawing.Size(101, 34)
        Me.IconButton3.TabIndex = 308
        Me.IconButton3.Text = "Oui"
        Me.IconButton3.UseVisualStyleBackColor = False
        '
        'yesorno
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(500, 200)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.IconButton3)
        Me.Controls.Add(Me.IconButton2)
        Me.Controls.Add(Me.IconButton1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximumSize = New System.Drawing.Size(500, 200)
        Me.Name = "yesorno"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "yesorno"
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents IconButton4 As FontAwesome.Sharp.IconButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents IconButton1 As FontAwesome.Sharp.IconButton
    Friend WithEvents IconButton2 As FontAwesome.Sharp.IconButton
    Friend WithEvents IconButton3 As FontAwesome.Sharp.IconButton
End Class
