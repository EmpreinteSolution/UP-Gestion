<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class barcodgen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(barcodgen))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.imgGenCode = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.imgIDAutomation = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.prnt_prev = New System.Windows.Forms.PrintPreviewDialog()
        Me.prnt_doc = New System.Drawing.Printing.PrintDocument()
        Me.nud_MarginWBrcode = New System.Windows.Forms.NumericUpDown()
        Me.nud_MarginHBrcode = New System.Windows.Forms.NumericUpDown()
        Me.nud_MarginW = New System.Windows.Forms.NumericUpDown()
        Me.nud_MarginH = New System.Windows.Forms.NumericUpDown()
        CType(Me.imgGenCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.imgIDAutomation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_MarginWBrcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_MarginHBrcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_MarginW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_MarginH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(167, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(317, 29)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Welcome Code Scratcher!"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(51, 332)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(301, 72)
        Me.btnPrint.TabIndex = 21
        Me.btnPrint.Text = "Print Barcode"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnGenerate
        '
        Me.btnGenerate.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.Location = New System.Drawing.Point(393, 54)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(171, 75)
        Me.btnGenerate.TabIndex = 20
        Me.btnGenerate.Text = "Generate" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Barcode"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(58, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 25)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Weight :"
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Location = New System.Drawing.Point(161, 99)
        Me.txtWeight.Multiline = True
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(226, 30)
        Me.txtWeight.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(83, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 25)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Text :"
        '
        'txtBarcode
        '
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcode.Location = New System.Drawing.Point(161, 54)
        Me.txtBarcode.Multiline = True
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(226, 30)
        Me.txtBarcode.TabIndex = 16
        '
        'imgGenCode
        '
        Me.imgGenCode.Location = New System.Drawing.Point(338, 200)
        Me.imgGenCode.Name = "imgGenCode"
        Me.imgGenCode.Size = New System.Drawing.Size(241, 97)
        Me.imgGenCode.TabIndex = 15
        Me.imgGenCode.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(332, 166)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(267, 20)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Method2: GenCode128 Barcode"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 166)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 20)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Method1: IDAutomation Barcode"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.imgIDAutomation)
        Me.Panel1.Location = New System.Drawing.Point(47, 185)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(242, 112)
        Me.Panel1.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 15)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "12365"
        '
        'imgIDAutomation
        '
        Me.imgIDAutomation.Location = New System.Drawing.Point(0, 15)
        Me.imgIDAutomation.Name = "imgIDAutomation"
        Me.imgIDAutomation.Size = New System.Drawing.Size(241, 104)
        Me.imgIDAutomation.TabIndex = 14
        Me.imgIDAutomation.TabStop = False
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(357, 332)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(301, 72)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = "Print Barcode"
        Me.Button1.UseVisualStyleBackColor = True
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
        'nud_MarginWBrcode
        '
        Me.nud_MarginWBrcode.DecimalPlaces = 2
        Me.nud_MarginWBrcode.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_MarginWBrcode.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nud_MarginWBrcode.Location = New System.Drawing.Point(562, 193)
        Me.nud_MarginWBrcode.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nud_MarginWBrcode.Name = "nud_MarginWBrcode"
        Me.nud_MarginWBrcode.Size = New System.Drawing.Size(97, 27)
        Me.nud_MarginWBrcode.TabIndex = 105
        Me.nud_MarginWBrcode.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'nud_MarginHBrcode
        '
        Me.nud_MarginHBrcode.DecimalPlaces = 2
        Me.nud_MarginHBrcode.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_MarginHBrcode.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.nud_MarginHBrcode.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nud_MarginHBrcode.Location = New System.Drawing.Point(561, 157)
        Me.nud_MarginHBrcode.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nud_MarginHBrcode.Name = "nud_MarginHBrcode"
        Me.nud_MarginHBrcode.Size = New System.Drawing.Size(97, 27)
        Me.nud_MarginHBrcode.TabIndex = 106
        Me.nud_MarginHBrcode.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'nud_MarginW
        '
        Me.nud_MarginW.DecimalPlaces = 2
        Me.nud_MarginW.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_MarginW.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nud_MarginW.Location = New System.Drawing.Point(560, 98)
        Me.nud_MarginW.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nud_MarginW.Name = "nud_MarginW"
        Me.nud_MarginW.Size = New System.Drawing.Size(97, 27)
        Me.nud_MarginW.TabIndex = 103
        Me.nud_MarginW.Value = New Decimal(New Integer() {14, 0, 0, 0})
        '
        'nud_MarginH
        '
        Me.nud_MarginH.DecimalPlaces = 1
        Me.nud_MarginH.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_MarginH.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nud_MarginH.Location = New System.Drawing.Point(561, 57)
        Me.nud_MarginH.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nud_MarginH.Name = "nud_MarginH"
        Me.nud_MarginH.Size = New System.Drawing.Size(97, 27)
        Me.nud_MarginH.TabIndex = 104
        Me.nud_MarginH.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'barcodgen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 422)
        Me.Controls.Add(Me.nud_MarginWBrcode)
        Me.Controls.Add(Me.nud_MarginHBrcode)
        Me.Controls.Add(Me.nud_MarginW)
        Me.Controls.Add(Me.nud_MarginH)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBarcode)
        Me.Controls.Add(Me.imgGenCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "barcodgen"
        Me.Text = "Code Scratcher"
        CType(Me.imgGenCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.imgIDAutomation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_MarginWBrcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_MarginHBrcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_MarginW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_MarginH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents imgGenCode As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents imgIDAutomation As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents PageSetupDialog1 As PageSetupDialog
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents prnt_prev As PrintPreviewDialog
    Friend WithEvents prnt_doc As Printing.PrintDocument
    Friend WithEvents nud_MarginWBrcode As NumericUpDown
    Friend WithEvents nud_MarginHBrcode As NumericUpDown
    Friend WithEvents nud_MarginW As NumericUpDown
    Friend WithEvents nud_MarginH As NumericUpDown
End Class
