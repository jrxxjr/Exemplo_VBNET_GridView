Public Class Form2
    Private Painel As New Panel
    Private WithEvents meuDataGridView As New DataGridView
    Private WithEvents incluiNovaLinhaButton As New Button
    Private WithEvents deletaLinhaButton As New Button
    Private WithEvents pesquisaNoGrid As New TextBox
    Private WithEvents pesquisa As New Button

    Public Shared Sub Main()
        Application.EnableVisualStyles()

    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Criando um GridView via código"

        'define o leiaute, a configuração e preenche o datagridview
        defineLayout()
        configuraDataGridView()
        preencheDataGridView()

    End Sub
    Private Sub defineLayout()

        'define o tamanho do painel e inclui os botões : deletar e incluir linha
        Me.Size = New Size(450, 250)

        With incluiNovaLinhaButton
            .Text = "Inclui Linha"
            .Location = New Point(10, 10)
        End With

        With deletaLinhaButton
            .Text = "Deleta Linha"
            .Location = New Point(100, 10)
        End With

        With pesquisaNoGrid
            .Width = 150
            .Location = New Point(200, 10)
        End With

        With pesquisa
            .Text = "Procura"
            .Location = New Point(350, 10)
        End With

        With Painel
            .Controls.Add(incluiNovaLinhaButton)
            .Controls.Add(deletaLinhaButton)
            .Controls.Add(pesquisaNoGrid)
            .Controls.Add(pesquisa)
            .Height = 50
            .Dock = DockStyle.Bottom
        End With

        Me.Controls.Add(Me.Painel)
    End Sub
    Private Sub configuraDataGridView()

        Me.Controls.Add(meuDataGridView)
        meuDataGridView.ColumnCount = 3

        With meuDataGridView.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Tomato
            .ForeColor = Color.White
            .Font = New Font(meuDataGridView.Font, FontStyle.Bold)
        End With

        'define o nome, tamanho , inclui colunas e linha no gridview
        With meuDataGridView
            .Name = "meuDataGridView"
            .Location = New Point(8, 8)
            .Size = New Size(250, 150)
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            .CellBorderStyle = DataGridViewCellBorderStyle.Single
            .GridColor = Color.Black
            .RowHeadersVisible = False

            'define 3 colunas : codigo, nome e nascimento
            .Columns(0).Name = "Codigo"
            .Columns(1).Name = "Nome"
            .Columns(2).Name = "Nascimento"
            .Columns(2).Width = 200
            .Columns(2).DefaultCellStyle.Font = New Font(Me.meuDataGridView.DefaultCellStyle.Font, FontStyle.Italic)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .Dock = DockStyle.Fill
        End With

    End Sub

    Private Sub preencheDataGridView()

        'preenche o grid com alguns dados
        Dim row0 As String() = {"1", "Macoratti", "11/02/1968"}
        Dim row1 As String() = {"2", "Jefferson", "12/09/1995"}
        Dim row2 As String() = {"3", "Jessica", "11/11/1971"}
        Dim row3 As String() = {"4", "Janice", "06/07/1990"}
        Dim row4 As String() = {"5", "Mirima", "5/07/1981"}

        'adiciona as linhas
        With Me.meuDataGridView.Rows
            .Add(row0)
            .Add(row1)
            .Add(row2)
            .Add(row3)
            .Add(row4)
        End With

        'adiciona as colunas
        With Me.meuDataGridView
            .Columns(0).DisplayIndex = 0
            .Columns(1).DisplayIndex = 1
            .Columns(2).DisplayIndex = 2
        End With

    End Sub
    Private Sub incluiNovaLinhaButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles incluiNovaLinhaButton.Click
        'inclui uma nova linha no grid
        Me.meuDataGridView.Rows.Add()
    End Sub

    Private Sub deletaLinhaButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles deletaLinhaButton.Click
        'verifica se a linha a ser excluida é valida
        If Me.meuDataGridView.SelectedRows.Count > 0 AndAlso Not Me.meuDataGridView.SelectedRows(0).Index = Me.meuDataGridView.Rows.Count - 1 Then
            Me.meuDataGridView.Rows.RemoveAt(Me.meuDataGridView.SelectedRows(0).Index)
        End If

    End Sub
    Private Sub meuDataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) _
Handles meuDataGridView.CellFormatting

        'verifica se o nome da coluna é Nascimento
        If Me.meuDataGridView.Columns(e.ColumnIndex).Name = "Nascimento" Then
            'se o valor informado for válido formata a data
            If e IsNot Nothing Then
                If e.Value IsNot Nothing Then
                    Try
                        e.Value = DateTime.Parse(e.Value.ToString()).ToLongDateString()
                        e.FormattingApplied = True
                    Catch ex As FormatException
                        MsgBox("Data invalida.", e.Value.ToString())
                    End Try
                End If
            End If
        End If
    End Sub
    Private Sub pesquisa_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pesquisa.Click

        Dim texto As String = Nothing

        If pesquisaNoGrid.Text <> String.Empty Then
            'percorre cada linha do DataGridView
            For Each linha As DataGridViewRow In meuDataGridView.Rows
                'percorre cada célula da linha
                For Each celula As DataGridViewCell In meuDataGridView.Rows(linha.Index).Cells
                    'se a coluna for a coluna 1 (Nome) então verifica o criterio
                    If celula.ColumnIndex = 1 Then
                        texto = celula.Value.ToString.ToLower
                        'se o texto informado estiver contido na célula então seleciona toda linha
                        If texto.Contains(pesquisaNoGrid.Text.ToLower) Then
                            'seleciona a linha
                            Me.meuDataGridView.Rows(celula.RowIndex).Selected = True
                            Exit Sub
                        End If
                    End If
                Next
            Next
        End If
    End Sub

End Class