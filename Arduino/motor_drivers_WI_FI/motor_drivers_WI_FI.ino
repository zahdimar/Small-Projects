#include <SPI.h>
#include "nRF24L01.h"
#include "RF24.h"

#define PWMA 3
#define PWMB 3
#define AIN1 6
#define AIN2 7
#define BIN1 5
#define BIN2 4
#define STBY 2
#define motorSpeedStep 50

#define FORWARD_KEY    0xFF18E7
#define BACKWARD_KEY   0xFF38C7
#define LEFT_KEY       0xFF10EF
#define RIGHT_KEY      0xFF5AA5
#define SPEED_UP_KEY   0xFFA857
#define SPEED_DOWN_KEY 0xFFE01F

int motorSpeed = 250;

RF24 radio(9,10);
         // Topology
byte address[][6] = {"1Node","2Node","3Node","4Node","5Node","6Node"};  //возможные номера труб

typedef enum { role_ping_out = 1, role_pong_back } role_e;                 // The various roles supported by this sketch
const char* role_friendly_name[] = { "invalid", "Ping out", "Pong back"};  // The debug-friendly names of those roles
role_e role = role_pong_back;                                 


void MoveForward(int motorSpeed) {
  digitalWrite(AIN1, LOW);
  digitalWrite(AIN2, HIGH);
  digitalWrite(BIN1, LOW);
  digitalWrite(BIN2, HIGH);
  analogWrite(PWMB, motorSpeed);
  analogWrite(PWMA, motorSpeed);
}

void MoveBackward(int motorSpeed) {
  digitalWrite(AIN1, HIGH);
  digitalWrite(AIN2, LOW);
  digitalWrite(BIN1, HIGH);
  digitalWrite(BIN2, LOW);
  analogWrite(PWMB, motorSpeed);
  analogWrite(PWMA, motorSpeed);
}

void TurnRight(int motorSpeed) {
  digitalWrite(AIN1, HIGH);
  digitalWrite(AIN2, LOW);
  digitalWrite(BIN1, LOW);
  digitalWrite(BIN2, HIGH);
  analogWrite(PWMB, motorSpeed);
  analogWrite(PWMA, motorSpeed);
}

void TurnLeft(int motorSpeed) {
  digitalWrite(AIN1, LOW);
  digitalWrite(AIN2, HIGH);
  digitalWrite(BIN1, HIGH);
  digitalWrite(BIN2, LOW);
  analogWrite(PWMB, motorSpeed);
  analogWrite(PWMA, motorSpeed);
}

void UpdateMotorSpeed(int newValue) {
  if (newValue > 100 && newValue < 1000) {
    motorSpeed = newValue;
  }
}

void setup() {
  pinMode(PWMA, OUTPUT);
  pinMode(PWMB, OUTPUT);
  pinMode(AIN1, OUTPUT);
  pinMode(AIN2, OUTPUT);
  pinMode(BIN1, OUTPUT);
  pinMode(BIN2, OUTPUT);
  pinMode(STBY, OUTPUT);
  Serial.begin(9600);
  digitalWrite(STBY, HIGH);

  radio.begin();
    radio.begin(); //активировать модуль
  radio.setAutoAck(1);         //режим подтверждения приёма, 1 вкл 0 выкл
  radio.setRetries(0,15);     //(время между попыткой достучаться, число попыток)
  radio.enableAckPayload();    //разрешить отсылку данных в ответ на входящий сигнал
  radio.setPayloadSize(32);     //размер пакета, в байтах

  radio.openReadingPipe(1,address[0]);      //хотим слушать трубу 0
  radio.setChannel(0x5d);  //выбираем канал (в котором нет шумов!)

  radio.setPALevel (RF24_PA_MAX); //уровень мощности передатчика. На выбор RF24_PA_MIN, RF24_PA_LOW, RF24_PA_HIGH, RF24_PA_MAX
  radio.setDataRate (RF24_1MBPS); //скорость обмена. На выбор RF24_2MBPS, RF24_1MBPS, RF24_250KBPS
  //должна быть одинакова на приёмнике и передатчике!
  //при самой низкой скорости имеем самую высокую чувствительность и дальность!!
  // ВНИМАНИЕ!!! enableAckPayload НЕ РАБОТАЕТ НА СКОРОСТИ 250 kbps!
  
  radio.powerUp(); //начать работу
  radio.startListening();  //начинаем слушать эфир, мы приёмный модуль
}

void loop() {
    byte pipeNo;                          
    byte rec_val;
    while( radio.available(&pipeNo)){    // слушаем эфир со всех труб
      radio.read( &rec_val, sizeof(rec_val));         // чиатем входящий сигнал
      radio.writeAckPayload(pipeNo,&rec_val, 2 );  // отправляем обратно то что приняли
    
      byte real_x = rec_val % 10;
      byte real_y = (rec_val - real_x) / 10;
      int x = map(real_x, 0, 10, -5, 5);
      int y = map(real_y, 0, 10, -5, 5);

      Serial.print("x: "); Serial.println(x);
      Serial.print("y: "); Serial.println(y);

      if (x !=0 || y !=0) {
         digitalWrite(STBY, HIGH);
      }
      else {
         digitalWrite(STBY, LOW);
      }


      if (y > 0) {
        MoveForward(1000);
      }
      if (y < 0) {
        MoveBackward(1000);
      }
      if (x > 0) {
        TurnRight(1000);
      }
      if ( x < 0) {
        TurnLeft(1000);
      }
   }
  
  /*MoveForward(250);
  delay(1000);
  TurnLeft(250);
  delay(1000);
  MoveForward(250);
  delay(1000);
  MoveBackward(250);
  delay(1000);*/
}
