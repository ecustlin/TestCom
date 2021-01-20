using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FPCheckCom
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Guid("F32F4471-A004-4C75-9964-84854BC199E7")]
    public class VerifyClass
    {
        IntPtr m_hDevice = IntPtr.Zero;

        [DllImport("ZKFPModule.dll")]
        extern static IntPtr ZKFPModule_Connect(string lpParams);

        [DllImport("ZKFPModule.dll")]
        extern static int ZKFPModule_Disconnect(IntPtr Handle);

        [DllImport("ZKFPModule.dll")]
        extern static int ZKFPModule_Verify(IntPtr Handle, int userID);

        [DllImport("ZKFPModule.dll")]
        extern static int ZKFPModule_GetUser(IntPtr Handle, int userID, StringBuilder name, string password,
                                                ref ushort secLevel, ref UInt32 PIN2,
                                                    byte[] privilege, byte[] figerprintNum, byte[] Card);

        [DllImport("ZKFPModule.dll")]
        extern static int ZKFPModule_GetStatus(IntPtr Handle);

        [DllImport("ZKFPModule.dll")]
        extern static int ZKFPModule_FreeScan(IntPtr Handle, ref int UserID, ref int Index);


        //connect by usb
        public IntPtr Connect()
        {
            if (IntPtr.Zero != m_hDevice)
            {
                return m_hDevice;
            }
            m_hDevice = ZKFPModule_Connect("protocol=USB,vendor-id=6997,product-id=289");
            return m_hDevice;
        }

        //connect by user control
        public IntPtr ConnectByStr(String constr)
        {
            if (IntPtr.Zero != m_hDevice)
            {
                return m_hDevice;
            }
            m_hDevice = ZKFPModule_Connect(constr);
            return m_hDevice;
        }

        //DisConnect
        public int DisConnect()
        {
            if (IntPtr.Zero == m_hDevice)
            {
                return 0;
            }

            int nRet = ZKFPModule_Disconnect(m_hDevice);
            
            if (0 == nRet)
            {
                m_hDevice = IntPtr.Zero;
            }

            return nRet;
         }

        //check and return the result of live20m
        public int Verify(int userID)
        {
            int nRet = ZKFPModule_Verify(m_hDevice, userID);
            return nRet;
        }

        //get the userID and finger index from the device
        public int FreeScan()
        {
            int UserID = 0;
            int Index = 0;
            int nRet = -1;
            Boolean m_bStop = false;

            Task.Factory.StartNew(() =>
            {
                while (!m_bStop)
                {
                    UserID = 0;
                    Index = 0;

                    nRet = ZKFPModule_FreeScan(m_hDevice, ref UserID, ref Index);

                    if (nRet == 0)
                    {
                        m_bStop = true;
                    }
                }
            } ).Wait(10 * 1000);

            return nRet;
        }

        //check status 0 success
        public int GetStatus()
        {
            if (IntPtr.Zero == m_hDevice)
            {
                return -1;
            }
            int nRet = ZKFPModule_GetStatus(m_hDevice);
            return nRet;
        }
    }
}
