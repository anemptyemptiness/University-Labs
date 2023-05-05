import socket
import threading

# клиент вводит никнейм
nickname = input("Choose your nickname: ")

# подключение к серверу по сокету
client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client.connect(('127.0.0.1', 55555))

# слушаем сервер и отправляем никнейм
def receive():  # получаем
    while True:
        try:
            # получаем сообщение от сервера
            # If 'NICK' отправляем никнейм
            message = client.recv(1024).decode('ascii')
            if message == 'NICK':
                client.send(nickname.encode('ascii'))
            else:
                print(message)
        except:
            # закрываем соединение с ошибкой
            print("Обнаружена ошибка!")
            client.close()
            break

# отправляем сообщение на сервер
def write():
    while True:
        message = '{}: {}'.format(nickname, input(''))
        client.send(message.encode('ascii'))

# открываем потоки для прослушивания сервера и печатания сообщений
receive_thread = threading.Thread(target=receive)
receive_thread.start()

write_thread = threading.Thread(target=write)
write_thread.start()

