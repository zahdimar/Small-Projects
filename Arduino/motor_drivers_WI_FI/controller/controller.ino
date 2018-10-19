#include <SPI.h>
#include "nRF24L01.h"
#include "RF24.h"

#define X_AXIS A0
#define Y_AXIS A1

RF24 radio(9,10); // "создать" модуль на пинах 9 и 10 Для Уно

byte address[][6] = {"1Node","2Node","3Node","4Node","5Node","6Node"};  //возможные номера труб

byte counter;

void setup(){
  Serial.begin(9600); //открываем порт для связи с ПК
  pinMode(7, OUTPUT); //+5v
  digitalWrite(7, HIGH);

  pinMode(X_AXIS, INPUT);
  pinMode(Y_AXIS, INPUT);
  radio.begin(); //активировать модуль
  radio.setAutoAck(1);         //режим подтверждения приёма, 1 вкл 0 выкл
  radio.setRetries(0,15);     //(время между попыткой достучаться, число попыток)
  radio.enableAckPayload();    //разрешить отсылку данных в ответ на входящий сигнал
  radio.setPayloadSize(32);     //размер пакета, в байтах

  radio.openWritingPipe(address[0]);   //мы - труба 0, открываем канал для передачи данных
  radio.setChannel(0x5d);  //выбираем канал (в котором нет шумов!)

  radio.setPALevel (RF24_PA_MAX); //уровень мощности передатчика. На выбор RF24_PA_MIN, RF24_PA_LOW, RF24_PA_HIGH, RF24_PA_MAX
  radio.setDataRate (RF24_1MBPS); //скорость обмена. На выбор RF24_2MBPS, RF24_1MBPS, RF24_250KBPS
  //должна быть одинакова на приёмнике и передатчике!
  //при самой низкой скорости имеем самую высокую чувствительность и дальность!!
  // ВНИМАНИЕ!!! enableAckPayload НЕ РАБОТАЕТ НА СКОРОСТИ 250 kbps!
  
  radio.powerUp(); //начать работу
  radio.stopListening();  //не слушаем радиоэфир, мы передатчик
}

void loop(void) {    
  unsigned long last_time = micros();         //запоминаем время отправки
  int x = analogRead(X_AXIS);
  int y = analogRead(Y_AXIS);
  x = map(x, 0, 1024, 0, 10);
  y = map(y, 0, 1024, 0, 10);
  byte result_to_send = x * 10 + y;
  Serial.print("Sending... ");Serial.println(result_to_send);
  if ( radio.write(&result_to_send, sizeof(result_to_send))){                 //отправляем значение counter
    if(!radio.available()){                     //если получаем пустой ответ
      Serial.print("Empty, "); Serial.print(" Time: "); Serial.print(micros()-last_time); Serial.println(" microseconds"); Serial.println();
    }
  }else{   Serial.println("Fail"); }    
  delay(500);
  
}
