# -*- coding: utf-8 -*-
import config #be sure about config file exists 
import telebot
import time

bot = telebot.TeleBot(config.token)
chatState = {};

class UserState:
    name = ""
    age = -1

@bot.message_handler(commands=['help', 'start'])
def help(message):
    bot.send_message(message.chat.id, "Для знакомства введите /greetings")
    bot.send_message(message.chat.id, "Если вы думаете, что я вас знаю - введите /who")
    bot.send_message(message.chat.id, "Если вы хотите удалить информацию о себе - введите /reset")


@bot.message_handler(commands=['who'])
def who(message):
    if message.chat.id not in chatState or not chatState[message.chat.id]:
        bot.send_message(message.chat.id, "Я вас не знаю, давайте познакомимся. Для знакомства введите /greetings")
    else:
        bot.send_message(message.chat.id, formatInfo(chatState[message.chat.id]))


@bot.message_handler(commands=['reset'])
def reset(message):
    chatState[message.chat.id] = None
    bot.send_message(message.chat.id, "Я забыл о тебе, но надеюсь мы ещё встретимся")
    bot.send_message(message.chat.id, "Если вдруг захочешь общаться- введи /start")


@bot.message_handler(commands=['greetings'])
def start(message):
    bot.send_message(message.chat.id, "Привет, меня зовут Димка-бот")
    time.sleep(2)
    bot.send_message(message.chat.id, "А как зовут тебя?")
    chatState[message.chat.id] = UserState();


@bot.message_handler(func=lambda message: message.chat.id in  chatState and chatState[message.chat.id].name == "" )
def nameInput(message):
    text = message.text;
    chatState[message.chat.id].name = text;

    if text == "Руслан":
        bot.send_message(message.chat.id, "О, здорова Руссо!")
        bot.send_message(message.chat.id, "Скок тебе годиков?")
        return
    elif text == "Денис":
        bot.send_message(message.chat.id, "Привет, Женис Серебрянный!")
        bot.send_message(message.chat.id, "На сколько ты стар? Давай только в цифрах")
        return
    elif text == "Наташа" or text == "Наталия":
        bot.send_message(message.chat.id, "Привет, Натахтари!")
        bot.send_message(message.chat.id, "Прости, но я должен это спросить: сколько тебе лет?")
        return

    bot.send_message(message.chat.id, "Приятно познаковиться, %s" % text)
    bot.send_message(message.chat.id, "Вы так же можете ввести свой возраст")


@bot.message_handler(func=lambda message: message.chat.id in  chatState and chatState[message.chat.id].age == -1)
def ageInput(message):
    if not message.text.isdigit():
        bot.send_message(message.chat.id, "Что-то не так, попробуй ещё раз!")
        return

    if int(message.text) < 5 or int(message.text) > 100:
        bot.send_message(message.chat.id, "Какой-то странный возраст. Не верю! Отвечай честно.")
        return
    else:
        chatState[message.chat.id].age = int(message.text)
        bot.send_message(message.chat.id, "Однако. Не так часто можно встретить человека старше себя...")
        time.sleep(1)
        bot.send_message(message.chat.id, "C учётом того, что мне нет и одной недели...")
        time.sleep(1)
        age = chatState[message.chat.id].age
        bot.send_message(message.chat.id, "Теперь я знаю всё о тебе. \n%s" % formatInfo(chatState[message.chat.id]))
        time.sleep(1)
        bot.send_message(message.chat.id, "на данный момент, это всё что я могу")
        bot.send_message(message.chat.id, "Введите для общения заново /start")


def formatInfo(info):
    infoText = ""
    if info.name:
        infoText += "Тебя зовут %s. " % info.name
    if info.age > 0:
        infoText += "Тебе %s" % str(info.age) + (" года" if info.age % 10 < 5 else " лет")
    return infoText;


if __name__ == '__main__':
     bot.polling(none_stop=True)