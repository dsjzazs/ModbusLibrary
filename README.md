ModbusLibrary主要依据《Modbus协议中文版【完整版】》编写的代码.

┏━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━┳━━━━━━━━━━━━
┃   类型      ┃    函数名称    ┃  功能码        
┣━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━╋━━━━━━━━━━━━
┃物理离散量输入┃ 读输入离散量   ┃    02    √    
┣━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━╋━━━━━━━━━━━━
┃             ┃ 读入线圈       ┃    01   √     
┃    线圈     ┃ 写单个线圈     ┃    05   √     
┃             ┃ 写多个线圈     ┃    15   √     
┣━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━╋━━━━━━━━━━━━
┃    寄存器    ┃ 读输入寄存器   ┃    04   √     
┣━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━╋━━━━━━━━━━━━
┃             ┃ 读多个寄存器   ┃    03   √     
┃             ┃ 写单个寄存器   ┃    06   √     
┃             ┃ 写多个寄存器   ┃    16   √     
┃   寄存器     ┃ 读/写多个寄存器┃    23         
┃             ┃ 屏蔽写寄存器   ┃    22         
┃             ┃ 读文件记录     ┃    20         
┣━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━╋━━━━━━━━━━━━
┃文件记录访问  ┃ 写文件记录     ┃    21         
┣━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━╋━━━━━━━━━━━━
┃             ┃ 读设备识别码   ┃    43         
┗━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━┻━━━━━━━━━━━━
