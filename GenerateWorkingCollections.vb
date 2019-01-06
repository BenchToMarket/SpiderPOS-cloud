Imports DataSet_Builder

Module GenerateWorkingCollections


    Friend Sub AddEmployeeToCollections(ByRef newemployee As Employee)
        Dim emp As Employee
        Dim alreadyOnFloor As Boolean

        Try
            AddEmployeeToWorkingCollection(newemployee)

            If newemployee.Bartender = True Then
                currentBartenders.Add(newemployee)
            End If
            If newemployee.Server = True Then
                currentServers.Add(newemployee)
            End If
            If newemployee.Manager = True Then
                currentManagers.Add(newemployee)
            End If

            If newemployee.Bartender = True Or newemployee.Server = True Or newemployee.Manager = True Then
                For Each emp In todaysFloorPersonnel
                    '   checking for duplicates
                    If emp.EmployeeID = newemployee.EmployeeID Then
                        alreadyOnFloor = True
                        Exit For
                    End If
                Next
                If alreadyOnFloor = False Then
                    todaysFloorPersonnel.Add(newemployee)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Friend Sub RemoveEmployeeFromCollection(ByVal empID As Integer)
        Dim currentEmployee As Employee

        For Each currentEmployee In workingEmployees
            If currentEmployee.EmployeeID = empID Then
                Exit For
            End If
        Next

        Try
            If currentEmployee.Bartender = True Then
                currentBartenders.Remove(currentEmployee)
            End If
            If currentEmployee.Server = True Then
                currentServers.Remove(currentEmployee)
            End If
            If currentEmployee.Manager = True Then
                currentManagers.Remove(currentEmployee)
            End If

            If currentEmployee.Bartender = True Or currentEmployee.Server = True Or currentEmployee.Manager = True Then
                '             todaysFloorPersonnel.Remove(currentEmployee)
            End If
            RemoveEmployeeFromWorkingCollection(currentEmployee)
        Catch ex As Exception

        End Try


    End Sub

    Friend Sub AddEmployeeToSwipeCodeEmployeesEmployeeCollection(ByRef newEmployee As Employee)

        SwipeCodeEmployees.Add(newEmployee)

    End Sub

    Friend Sub AddEmployeeToAllEmployeeCollection(ByRef newEmployee As Employee)

        AllEmployees.Add(newEmployee)

    End Sub

    Friend Sub AddEmployeeToAllFloorCollection(ByRef newEmployee As Employee)

        allFloorPersonnel.Add(newEmployee)

    End Sub

    Friend Sub AddEmployeeToSalariedEmployeeCollection(ByRef newEmployee As Employee)

        SalariedEmployees.Add(newEmployee)
        '444      If Not newEmployee.EmployeeID = 6986 Then
        'currentManagers.Add(newEmployee)
        '   End If

    End Sub

    Friend Sub AddEmployeeToWorkingCollection(ByRef newEmployee As Employee)
        '   _workingEMployees is a structure defined in term_Tahsc

        workingEmployees.Add(newEmployee)

    End Sub

    Friend Sub RemoveEmployeeFromWorkingCollection(ByRef currentEmployee As Employee)
        '   _workingEMployees is a structure defined in term_Tahsc

        workingEmployees.Remove(currentEmployee)

    End Sub

    Friend Sub LogInEmployeesEnteredWhenBackUp222()
        'this is different .. we only need this once
        'b/c working collections are the same on every terminal

        Dim currentEmployee As Employee

        For Each currentEmployee In workingEmployees
            If currentEmployee.dbUP = False And currentEmployee.ClockInReq = True Then
                Try
                    GenerateOrderTables.EnterEmployeeToLoginDataSet(currentEmployee)
                    currentEmployee.dbUP = True
                Catch ex As Exception

                End Try

            End If
        Next

    End Sub

End Module
