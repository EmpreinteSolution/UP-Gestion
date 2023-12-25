<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ventedetail
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.IconButton12 = New FontAwesome.Sharp.IconButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.IconButton6 = New FontAwesome.Sharp.IconButton()
        Me.IconButton3 = New FontAwesome.Sharp.IconButton()
        Me.IconButton2 = New FontAwesome.Sharp.IconButton()
        Me.IconButton1 = New FontAwesome.Sharp.IconButton()
        Me.IconButton4 = New FontAwesome.Sharp.IconButton()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.DataGridView3.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(127, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(107, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView3.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridView3.ColumnHeadersHeight = 22
        Me.DataGridView3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.Column3, Me.DataGridViewTextBoxColumn4, Me.Column2, Me.Column4})
        Me.DataGridView3.Cursor = System.Windows.Forms.Cursors.Arrow
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.Format = "N2"
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(107, Byte), Integer))
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView3.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridView3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.DataGridView3.EnableHeadersVisualStyles = False
        Me.DataGridView3.Location = New System.Drawing.Point(12, 59)
        Me.DataGridView3.Name = "DataGridView3"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView3.RowHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridView3.RowHeadersVisible = False
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.Format = "N2"
        DataGridViewCellStyle14.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView3.RowsDefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridView3.Size = New System.Drawing.Size(620, 401)
        Me.DataGridView3.TabIndex = 94
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column1.HeaderText = "Code"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 57
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.FillWeight = 113.0288!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Désignation"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn3.FillWeight = 113.0288!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Qté"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 48
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column3.HeaderText = "R%"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 46
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn4.FillWeight = 113.0288!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Montant"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 73
        '
        'Column2
        '
        Me.Column2.HeaderText = "marge"
        Me.Column2.Name = "Column2"
        Me.Column2.Visible = False
        '
        'Column4
        '
        Me.Column4.HeaderText = "gratuit"
        Me.Column4.Name = "Column4"
        Me.Column4.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.IconButton12)
        Me.Panel1.Location = New System.Drawing.Point(11, 10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(624, 43)
        Me.Panel1.TabIndex = 95
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(120, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 18)
        Me.Label4.TabIndex = 130
        Me.Label4.Text = "id"
        Me.Label4.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(197, 11)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 23)
        Me.DateTimePicker1.TabIndex = 122
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(467, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 18)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = "Ticket N° :"
        Me.Label3.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(5, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 15)
        Me.Label1.TabIndex = 120
        Me.Label1.Text = "Ticket N° :"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(82, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 15)
        Me.Label2.TabIndex = 119
        Me.Label2.Text = "ID"
        '
        'IconButton12
        '
        Me.IconButton12.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.IconButton12.Dock = System.Windows.Forms.DockStyle.Right
        Me.IconButton12.FlatAppearance.BorderSize = 0
        Me.IconButton12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton12.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IconButton12.ForeColor = System.Drawing.Color.White
        Me.IconButton12.IconChar = FontAwesome.Sharp.IconChar.Times
        Me.IconButton12.IconColor = System.Drawing.Color.White
        Me.IconButton12.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton12.IconSize = 25
        Me.IconButton12.Location = New System.Drawing.Point(580, 0)
        Me.IconButton12.Name = "IconButton12"
        Me.IconButton12.Size = New System.Drawing.Size(44, 43)
        Me.IconButton12.TabIndex = 118
        Me.IconButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.IconButton12.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(646, 10)
        Me.Panel2.TabIndex = 96
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 540)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(646, 10)
        Me.Panel3.TabIndex = 97
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(636, 10)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(10, 530)
        Me.Panel4.TabIndex = 98
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel5.Location = New System.Drawing.Point(0, 10)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(10, 530)
        Me.Panel5.TabIndex = 99
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(300, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 18)
        Me.Label6.TabIndex = 127
        Me.Label6.Text = "reste"
        Me.Label6.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(240, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 18)
        Me.Label5.TabIndex = 126
        Me.Label5.Text = "Mode"
        Me.Label5.Visible = False
        '
        'IconButton6
        '
        Me.IconButton6.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.IconButton6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IconButton6.FlatAppearance.BorderSize = 0
        Me.IconButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.IconButton6.ForeColor = System.Drawing.Color.White
        Me.IconButton6.IconChar = FontAwesome.Sharp.IconChar.Receipt
        Me.IconButton6.IconColor = System.Drawing.Color.White
        Me.IconButton6.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton6.IconSize = 24
        Me.IconButton6.Location = New System.Drawing.Point(481, 481)
        Me.IconButton6.Name = "IconButton6"
        Me.IconButton6.Size = New System.Drawing.Size(107, 37)
        Me.IconButton6.TabIndex = 129
        Me.IconButton6.Text = "BL"
        Me.IconButton6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IconButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.IconButton6.UseVisualStyleBackColor = False
        '
        'IconButton3
        '
        Me.IconButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.IconButton3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IconButton3.FlatAppearance.BorderSize = 0
        Me.IconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IconButton3.ForeColor = System.Drawing.Color.White
        Me.IconButton3.IconChar = FontAwesome.Sharp.IconChar.Receipt
        Me.IconButton3.IconColor = System.Drawing.Color.White
        Me.IconButton3.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton3.IconSize = 24
        Me.IconButton3.Location = New System.Drawing.Point(481, 481)
        Me.IconButton3.Name = "IconButton3"
        Me.IconButton3.Size = New System.Drawing.Size(107, 37)
        Me.IconButton3.TabIndex = 103
        Me.IconButton3.Text = "Facture"
        Me.IconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.IconButton3.UseVisualStyleBackColor = False
        Me.IconButton3.Visible = False
        '
        'IconButton2
        '
        Me.IconButton2.BackColor = System.Drawing.Color.ForestGreen
        Me.IconButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IconButton2.FlatAppearance.BorderSize = 0
        Me.IconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IconButton2.ForeColor = System.Drawing.Color.White
        Me.IconButton2.IconChar = FontAwesome.Sharp.IconChar.Receipt
        Me.IconButton2.IconColor = System.Drawing.Color.White
        Me.IconButton2.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton2.IconSize = 24
        Me.IconButton2.Location = New System.Drawing.Point(243, 236)
        Me.IconButton2.Name = "IconButton2"
        Me.IconButton2.Size = New System.Drawing.Size(83, 37)
        Me.IconButton2.TabIndex = 102
        Me.IconButton2.Text = "Bon"
        Me.IconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.IconButton2.UseVisualStyleBackColor = False
        Me.IconButton2.Visible = False
        '
        'IconButton1
        '
        Me.IconButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.IconButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IconButton1.FlatAppearance.BorderSize = 0
        Me.IconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.IconButton1.ForeColor = System.Drawing.Color.White
        Me.IconButton1.IconChar = FontAwesome.Sharp.IconChar.Edit
        Me.IconButton1.IconColor = System.Drawing.Color.White
        Me.IconButton1.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton1.IconSize = 24
        Me.IconButton1.Location = New System.Drawing.Point(257, 481)
        Me.IconButton1.Name = "IconButton1"
        Me.IconButton1.Size = New System.Drawing.Size(107, 37)
        Me.IconButton1.TabIndex = 101
        Me.IconButton1.Text = "Modifier"
        Me.IconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.IconButton1.UseVisualStyleBackColor = False
        '
        'IconButton4
        '
        Me.IconButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.IconButton4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IconButton4.FlatAppearance.BorderSize = 0
        Me.IconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IconButton4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.IconButton4.ForeColor = System.Drawing.Color.White
        Me.IconButton4.IconChar = FontAwesome.Sharp.IconChar.TrashAlt
        Me.IconButton4.IconColor = System.Drawing.Color.White
        Me.IconButton4.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.IconButton4.IconSize = 24
        Me.IconButton4.Location = New System.Drawing.Point(46, 481)
        Me.IconButton4.Name = "IconButton4"
        Me.IconButton4.Size = New System.Drawing.Size(100, 37)
        Me.IconButton4.TabIndex = 100
        Me.IconButton4.Text = "Annuler"
        Me.IconButton4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IconButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.IconButton4.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(416, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 18)
        Me.Label7.TabIndex = 130
        Me.Label7.Text = "caissier"
        '
        'ventedetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(646, 550)
        Me.Controls.Add(Me.IconButton6)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.IconButton3)
        Me.Controls.Add(Me.IconButton2)
        Me.Controls.Add(Me.IconButton1)
        Me.Controls.Add(Me.IconButton4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.DataGridView3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ventedetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Détails"
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents DataGridView3 As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents IconButton12 As FontAwesome.Sharp.IconButton
    Public WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents IconButton4 As FontAwesome.Sharp.IconButton
    Public WithEvents Label1 As Label
    Friend WithEvents IconButton1 As FontAwesome.Sharp.IconButton
    Public WithEvents Label3 As Label
    Friend WithEvents IconButton2 As FontAwesome.Sharp.IconButton
    Friend WithEvents IconButton3 As FontAwesome.Sharp.IconButton
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Public WithEvents Label6 As Label
    Public WithEvents Label5 As Label
    Friend WithEvents IconButton6 As FontAwesome.Sharp.IconButton
    Public WithEvents Label4 As Label
    Public WithEvents Label7 As Label
End Class
