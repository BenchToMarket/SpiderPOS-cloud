Public Class Training_UC

    Inherits System.Windows.Forms.UserControl


    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeOther()

    End Sub

    Friend Sub InitializeOther()

        Me.grdPreviousDailys.DataSource = dtTrainingDaily
        DeterminePreviousDailys()

    End Sub

    Private Sub DeterminePreviousDailys()

        sql.SqlTrainingDailyCodeSelect.Parameters("@LocationID").Value = companyInfo.LocationID
        sql.SqlTrainingDailyCodeSelect.Parameters("@DailyCode").Value = currentTerminal.CurrentDailyCode
        dsOrder.Tables("TrainingDaily").Clear()

        Try
             sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            sql.SqlTrainingDailyCode.Fill(dsOrder.Tables("TrainingDaily"))
            sql.cn.Close()
        Catch ex As Exception
            CloseConnection()
            MsgBox("Issues with Loading Previous Training Dailys: " & ex.Message)

          End Try

        If dsOrder.Tables("TrainingDaily").Rows.Count > 0 Then

        End If

    End Sub

    Private Function DeleteOrdersDetail(ByVal delDaily As Int64) As Boolean 'whereString As Int64) As Boolean
        'use above sub
        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try
            '          sql.cn.Open()
            '       sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand("DELETE FROM OpenOrders WHERE DailyCode = " & delDaily, sql.cn)
            dtr = cmd.ExecuteReader
            dtr.Read()
            dtr.Close()

            cmd = New SqlClient.SqlCommand("DELETE FROM OrderDetail WHERE DailyCode = " & delDaily, sql.cn)
            dtr = cmd.ExecuteReader
            dtr.Read()
            dtr.Close()
        
            cmd = New SqlClient.SqlCommand("DELETE FROM PaymentsAndCredits WHERE DailyCode = " & delDaily, sql.cn)
            dtr = cmd.ExecuteReader
            dtr.Read()
            dtr.Close()
       
        Catch ex As Exception
            MsgBox(ex.Message)
            '   dtr.Close()
            CloseConnection()
            Return False

        End Try

        '    dtr.Close()
        '    sql.cn.Close()
        Return True

    End Function

    Private Function DeleteExperienceTable(ByVal delDaily As Int64) As Boolean 'whereString As Int64) As Boolean
        'use above sub
        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try
            '          sql.cn.Open()
            '     sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand("DELETE FROM ExperienceTable WHERE DailyCode = " & delDaily, sql.cn)
            dtr = cmd.ExecuteReader
            dtr.Read()
            dtr.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            CloseConnection()
            Return False

        End Try

        '   sql.cn.Close()
        Return True

    End Function

    Private Function DeleteTerminalLogin(ByVal delDaily As Int64) As Boolean 'whereString As Int64) As Boolean
        'use above sub
        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try
            '        sql.cn.Open()
            '        sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand("DELETE FROM AAATerminalsOpen WHERE DailyCode = " & delDaily, sql.cn)
            dtr = cmd.ExecuteReader
            dtr.Read()
            dtr.Close()
            '          cmd = New SqlClient.SqlCommand("DELETE FROM AAATabOverview WHERE DailyCode = " & whereString, sql.cn)
            '         dtr = cmd.ExecuteReader
            '        dtr.Read()
            '     dtr.Close()
            cmd = New SqlClient.SqlCommand("DELETE FROM AAALoginTracking WHERE DailyCode = " & delDaily, sql.cn)
            dtr = cmd.ExecuteReader
            dtr.Read()
            dtr.Close()

        Catch ex As Exception
            CloseConnection()
            Return False

        End Try

        '     sql.cn.Close()
        Return True

    End Function

    Private Function DeleteDaily(ByVal delDaily As Int64) As Boolean 'whereString As Int64) As Boolean
        'use above sub
        Dim cmd As SqlClient.SqlCommand
        Dim dtr As SqlClient.SqlDataReader

        Try
            '        sql.cn.Open()
            '       sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
            cmd = New SqlClient.SqlCommand("DELETE FROM AAADailyBusiness WHERE DailyCode = " & delDaily, sql.cn)
            dtr = cmd.ExecuteReader
            dtr.Read()
            dtr.Close()

        Catch ex As Exception
            CloseConnection()
            Return False

        End Try

        '     sql.cn.Close()
        Return True

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Dispose()

    End Sub

    Private Sub btnDeleteTraining_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTraining.Click
        Dim didStep1Succeed As Boolean
        Dim didStep2Succeed As Boolean
        Dim didStep3Succeed As Boolean
        Dim didStep4Succeed As Boolean
        Dim whereString As String
        Dim countTraining As Integer
        Dim oRow As DataRow
        Dim isFirstRow As Boolean = True

        'not used now, except to say none are being deleted
        If dsOrder.Tables("TrainingDaily").Rows.Count > 0 Then
            For Each oRow In dsOrder.Tables("TrainingDaily").Rows
                If oRow("ActiveDaily") = True Then
                    If isFirstRow = True Then
                        whereString = oRow("DailyCode") '.ToString
                        isFirstRow = False
                    Else
                        whereString = whereString & " AND " & oRow("DailyCode") '.ToString
                    End If
                    countTraining += 1
                End If
            Next
            If whereString.Length < 1 Then
                MsgBox("No Dailys are marked as Training")
                Exit Sub 'means there are no dailys to delete
            End If
        Else
            Exit Sub
        End If

        Dim delDaily As Int64

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        Catch ex As Exception
            CloseConnection()
            MsgBox("Removing Training Daily Failed opening database")
            Exit Sub
        End Try

        If dsOrder.Tables("TrainingDaily").Rows.Count > 0 Then
            For Each oRow In dsOrder.Tables("TrainingDaily").Rows
                If oRow("ActiveDaily") = True Then
                    '        If isFirstRow = True Then
                    delDaily = oRow("DailyCode") '.ToString
                    didStep1Succeed = DeleteOrdersDetail(delDaily)
                    If didStep1Succeed = True Then
                        didStep2Succeed = DeleteExperienceTable(delDaily)
                        If didStep2Succeed = True Then
                            didStep3Succeed = DeleteTerminalLogin(delDaily)
                            If didStep3Succeed = True Then
                                didStep4Succeed = DeleteDaily(delDaily)
                                If didStep4Succeed = True Then
                                    'this means training dailys were deleted
                                    '         sql.cn.Close()
                                    '        MsgBox(countTraining & " Training Dailys were removed from the database")
                                Else
                                    'any failure sql.cn.close is in Catch
                                    MsgBox("Removing Training Daily Failed at the last step")
                                    Exit Sub
                                End If
                            Else
                                MsgBox("Removing Training Daily Failed after Experience Detail was deleted")
                                Exit Sub
                            End If
                        Else
                            MsgBox("Removing Training Daily Failed after Order Detail was deleted")
                            Exit Sub
                        End If
                    Else
                        MsgBox("Removing Training Daily Failed at the Start")
                        Exit Sub
                    End If
                    'End If
                End If
            Next
        End If


        '  sql.cn.Close()
        CloseConnection()
        MsgBox(countTraining & " Training Dailys were removed from the database")


    End Sub


    Private Sub btnDeleteTraining_Click222(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles btnDeleteTraining.Click
        Dim didStep1Succeed As Boolean
        Dim didStep2Succeed As Boolean
        Dim didStep3Succeed As Boolean
        Dim didStep4Succeed As Boolean
        Dim whereString As String
        Dim countTraining As Integer
        Dim oRow As DataRow
        Dim isFirstRow As Boolean = True

        If dsOrder.Tables("TrainingDaily").Rows.Count > 0 Then
            For Each oRow In dsOrder.Tables("TrainingDaily").Rows
                If oRow("ActiveDaily") = True Then
                    If isFirstRow = True Then
                        whereString = oRow("DailyCode") '.ToString
                        isFirstRow = False
                    Else
                        whereString = whereString & " AND " & oRow("DailyCode") '.ToString
                    End If
                    countTraining += 1
                End If
            Next
            If whereString.Length < 1 Then
                MsgBox("No Dailys are marked as Training")
                Exit Sub 'means there are no dailys to delete
            End If
        Else
            Exit Sub
        End If

        If dsOrder.Tables("TrainingDaily").Rows.Count > 0 Then
            For Each oRow In dsOrder.Tables("TrainingDaily").Rows
                If oRow("ActiveDaily") = True Then
                    If isFirstRow = True Then
                        whereString = oRow("DailyCode") '.ToString
                    End If
                End If
            Next
        End If

        Try
            sql.cn.Open()
            sql.sqlSelectAppRoleCommandPhoenix.ExecuteNonQuery()
        Catch ex As Exception
            CloseConnection()
            MsgBox("Removing Training Daily Failed opening database")
            Exit Sub
        End Try

        didStep1Succeed = DeleteOrdersDetail(whereString)
        If didStep1Succeed = True Then
            didStep2Succeed = DeleteExperienceTable(whereString)
            If didStep2Succeed = True Then
                didStep3Succeed = DeleteTerminalLogin(whereString)
                If didStep3Succeed = True Then
                    didStep4Succeed = DeleteDaily(whereString)
                    If didStep4Succeed = True Then
                        'this means training dailys were deleted
                        sql.cn.Close()
                        MsgBox(countTraining & " Training Dailys were removed from the database")
                    Else
                        'any failure sql.cn.close is in Catch
                        MsgBox("Removing Training Daily Failed at the last step")
                        Exit Sub
                    End If
                Else
                    MsgBox("Removing Training Daily Failed after Experience Detail was deleted")
                    Exit Sub
                End If
            Else
                MsgBox("Removing Training Daily Failed after Order Detail was deleted")
                Exit Sub
            End If
        Else
            MsgBox("Removing Training Daily Failed at the Start")
            Exit Sub
        End If
    End Sub


End Class
