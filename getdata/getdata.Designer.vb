<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class getdata
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
        Me.Label15 = New System.Windows.Forms.Label()
        Me.IconButton5 = New FontAwesome.Sharp.IconButton()
        Me.SuspendLayout()
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(263, 181)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(16, 18)
        Me.Label15.TabIndex = 232
        Me.Label15.Text = "0"
        '
        'IconButton5
        '
        Me.IconButton5.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.IconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton5.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IconButton5.ForeColor = System.Drawing.Color.White
        Me.IconButton5.IconChar = FontAwesome.Sharp.IconChar.Sync
        Me.IconButton5.IconColor = System.Drawing.Color.White
        Me.IconButton5.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton5.IconSize = 25
        Me.IconButton5.Location = New System.Drawing.Point(239, 117)
        Me.IconButton5.Name = "IconButton5"
        Me.IconButton5.Size = New System.Drawing.Size(86, 43)
        Me.IconButton5.TabIndex = 231
        Me.IconButton5.Text = "Vider"
        Me.IconButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.IconButton5.UseVisualStyleBackColor = False
        '
        'getdata
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.IconButton5)
        Me.Name = "getdata"
        Me.Text = "getdata"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label15 As Label
    Friend WithEvents IconButton5 As FontAwesome.Sharp.IconButton
End Class
