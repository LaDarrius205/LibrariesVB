Imports System.Data.SqlClient

Public Class SqlLibrary
    Private connectionString As String

    Public Sub New(connectionString As String)
        Me.connectionString = connectionString
    End Sub

    Public Function ExecuteNonQuery(query As String, Optional parameters As SqlParameter() = Nothing) As Integer
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Using command As New SqlCommand(query, connection)
                    If parameters IsNot Nothing Then
                        command.Parameters.AddRange(parameters)
                    End If

                    Return command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
            Return -1 ' Indicates an error occurred
        End Try
    End Function

    Public Function ExecuteQuery(query As String, Optional parameters As SqlParameter() = Nothing) As DataTable
        Dim resultTable As New DataTable()

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Using command As New SqlCommand(query, connection)
                    If parameters IsNot Nothing Then
                        command.Parameters.AddRange(parameters)
                    End If

                    Using adapter As New SqlDataAdapter(command)
                        adapter.Fill(resultTable)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try

        Return resultTable
    End Function
End Class
