function func_activate_obj(ClientHandle, Activate)
writeTCP(ClientHandle,sprintf("ActObj:%d",Activate));
pause(0.054);