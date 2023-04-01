function func_move_cam(ClientHandle, MoveX)
writeTCP(ClientHandle,sprintf("MoveCam:%f,%f",-MoveX,MoveX));
pause(0.054);