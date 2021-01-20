# FPCheckCom.dll 使用文档

### Dll注册

```
以管理员打开cmd
	RegAsm FPCheckCom.dll
依赖dll：ZKFPModule.dll、ZKSerialPort.dll
	复制到C:\Windows\System32以及C:\Windows\SysWOW64目录下
```

### Dll调用

```
VBS：
	Dim o
	Set o = CreateObject("FPCheckCom.VerifyClass")
	o.方法()
C#:
	[DllImport("FPCheckCom.dll")]
```

### live20m指纹模块连接及使用

```
接入设备，在设备管理器中找到对应的设备，右键更新驱动，然后选中设备驱动文件夹，确定后完成驱动安装，开始使用。
```

### Connect

```
[函数]
	 IntPtr Connect()
[功能]
	 通过USB连接指纹识别设备
[返回值]
	设备句柄
[备注]
	操作设备前请先连接设备。
```

### ConnectByStr

```
[函数]
	 IntPtr ConnectByStr(String constr)
[功能]
	 通过用户自定义连接字符连接指纹识别设备
[参数]
	constr
		（串口示例）protocol=RS232,port=COM1,baudrate=115200
		（USB示例）protocol=USB,vendor-id=6997,product-id=289
[返回值]
	设备句柄
[备注]
	操作设备前请先连接设备。
	protocol--协议类型，port--串口号，baudrate--波特率，vendor-id，product-id
```

### DisConnect

```
[函数]
	  int DisConnect()
[功能]
	  断开连接指纹识别设备
[返回值]
	  0成功，其他见错误说明(错误码)
```

### Verify

```
[函数]
	  int Verify(int userID)
[功能]
	  等待用户按手指从而进行1:1指纹模板验证
[参数]
	userID 用户ID
[返回值]
	0成功，其他见错误说明(错误码)
[备注]
	调用此函数时，需要传输UserID到模块，模块收到此命令后，等待用户按手指从而进行1:1的验证
```

### FreeScan

```
[函数]
	  int FreeScan()
[功能]
	  进行十秒循环指纹验证，超时返回验证失败
[参数]
	  userID 用户ID
[返回值]
	  0成功，其他见错误说明(错误码)
[备注]
	调用此函数时，需要传输UserID到模块，模块收到此命令后，等待用户按手指从而进行1:1的验证
```

### GetStatus

```
[函数]
	 int GetStatus()
[功能]
	 获取指纹设备的连接状态
[返回值]
	 0连接成功，其他见错误说明(错误码)
```

## 错误码

	#define  ZKFPM_OK					     0//操作成功
	#define  ZKFPM_FAIL	    			    -1//操作失败
	#define  ZKFPM_INVALID_PARAM		    -2//参数无效/错误
	#define  ZKFPM_NULL_POINTER		 	    -3//空指针
	#define  ZKFPM_MEMORY_NOT_ENOUGH	    -4//内存不足，分配失败
	#define  ZKFPM_INVALID_PROTOCOL	        -5//无效协议类型
	#define  ZKFPM_PROTOCOL_NOT_SUPPORT 	-6//协议不支持
	#define  ZKFPM_OPENDEVICE_FAIL			-7//连接设备失败
	#define  ZKFPM_DEVICE_NOT_OPENED		-8//设备未连接
	#define  ZKFPM_INVALID_HANDLE			-9//无效句柄
	#define  ZKFPM_TIMEOUT					-10//超时
	#define  ZKFPM_COMMU_FAIL				-11//通信失败
	#define  ZKFPM_FUN_NOT_SUPPORT			-12//接口不支持
	#define  ZKFPM_BUFFER_NOT_ENOUGH		-13//内存分配不足
	#define  ZKFPM_INVALID_IMAGE		  	-14//无效图像
	#define  ZKFPM_GET_TMP_FAIL				-15//采集模板失败
	#define  ZKFPM_FM_ERR_BEGIN	        -10000//固件错误编码
	//小于-10000错误码，对应的如下
	#define  FLAG_BUSY					0x34	//-10052  	//系统正在忙  
	#define  FLAG_FAIL					0x63	//-10099 	//失败
	#define  FLAG_TIME_OUT				0x64	//-10100	    //超时
	#define  FLAG_PARAM_ERROR			0x68	//-10104		//参数错误
	#define  FLAG_NOT_FOUND				0x69	//-10105		//找不到或不支持
	#define  FLAG_MEM_FULL				0x6D	//-10109		//存储容量超出
	#define  FLAG_FP_LIMIT				0x72	//-10114 	//超出允许登记的最大指纹数量
	#define  FLAG_INVALID_ID			0x76	//-10118		//无效的ID号
	#define  FLAG_CANCELED				0x81	//-10129 	//执行的命令被取消
	#define  FLAG_DATA_ERROR			0x82	//-10130		//传输数据错误
	#define  FLAG_EXIST_FINGER			0x86	//-10134 	//已经存在的指纹
	#define  FLAG_HEADCHECK_ERROR		0xFF	//-10255		//数据头校验失败（自定义）
	#define  FLAG_DATACHECK_ERROR		0xFE	//-10254		//数据校验失败（自定义）
	#define  FLAG_HEADTAG_ERROR			0xFD	//-10253		//数据头无效（自定义）

