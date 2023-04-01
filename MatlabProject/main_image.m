% Part 1 - display Image
clc;
clear all;
name = "Matlab";
Client = TCPInit('127.0.0.1',55001,name);
test = ImageReadTCP_One(Client,'Center');
imshow(test);
disp("Program done");

%% Part 2 - display Image and move Camera
clc;
clear all;
name = "Matlab";
Client = TCPInit('127.0.0.1',55001,name);

for move = 0 : 0.2 : 2
    func_move_cam(Client,move);
    test = ImageReadTCP_One(Client,'Center');
    imshow(test);
end

disp("Program done");

%% Part 3 - display Image, move Camera and hide Object for specific region
clc;
clear all;
name = "Matlab";
Client = TCPInit('127.0.0.1',55001,name);

for move = 0 : 0.2 : 2
    func_move_cam(Client,move);

    if move > 0.6 && move < 1.2
        act_obj = 0;
        func_activate_obj(Client,act_obj);
    else
        act_obj = 1;
        func_activate_obj(Client,act_obj);
    end   

    test = ImageReadTCP_One(Client,'Center');
    imshow(test);
end

disp("Program done");