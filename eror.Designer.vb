<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eror
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eror))
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.IconButton4 = New FontAwesome.Sharp.IconButton()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.IconButton4)
        Me.Panel5.Location = New System.Drawing.Point(15, 133)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(469, 61)
        Me.Panel5.TabIndex = 322
        '
        'IconButton4
        '
        Me.IconButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(46, Byte), Integer))
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
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(4, 196)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(492, 4)
        Me.Panel4.TabIndex = 321
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(496, 36)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(4, 164)
        Me.Panel3.TabIndex = 320
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 36)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(4, 164)
        Me.Panel2.TabIndex = 319
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
        Me.Label14.TabIndex = 318
        Me.Label14.Text = "Désignation :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Panel6.Controls.Add(Me.Label1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(500, 36)
        Me.Panel6.TabIndex = 316
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
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(10, 49)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(68, 55)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 317
        Me.PictureBox1.TabStop = False
        '
        'eror
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
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximumSize = New System.Drawing.Size(500, 200)
        Me.Name = "eror"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "eror"
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel5 As Panel
    Friend WithEvents IconButton4 As FontAwesome.Sharp.IconButton
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
