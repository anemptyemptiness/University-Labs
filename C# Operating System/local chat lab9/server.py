import socket
import threading

# создаем локальный хост и свободный порт
host = '127.0.0.1'
port = 55555

# создаем сервер
server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind((host, port))
server.listen()

# списки для клиентов и их никнеймов
clients = []
nicknames = []

# отправляем сообщения всем клиентам
def broadcast(message):
    for client in clients:
        client.send(message)

# обработка сообщений для клиентов
def handle(client):
    while True:
        try:
            # выводим сообщение
            message = client.recv(1024)
            broadcast(message)
        except:
            # удаляем и закрываем клиента
            index = clients.index(client)
            clients.remove(client)
            client.close()
            nickname = nicknames[index]
            broadcast('{} left!'.format(nickname).encode('ascii'))
            nicknames.remove(nickname)
            break

def receive():
    while True:
        # принимаем подключение
        client, address = server.accept()  # принимаем сокет и адрес клиента
        print("Connected with {}".format(str(address)))

        # запрос и сохранение никнейма
        client.send('NICK'.encode('ascii'))
        nickname = client.recv(1024).decode('ascii')  # получаем данные от клиента в буфер порциями 1 кб, кодируем
        nicknames.append(nickname)
        clients.append(client)

        # печатаем и выводим никнейм
        print("Nickname is {}".format(nickname))  # печать для сервера
        broadcast("{} joined!".format(nickname).encode('ascii'))  # для всех клиентов
        client.send(' Connected to server!'.encode('ascii'))

        # запускаем поток для клиента
        thread = threading.Thread(target=handle, args=(client,))
        thread.start()

if __name__ == '__main__':
    receive()

