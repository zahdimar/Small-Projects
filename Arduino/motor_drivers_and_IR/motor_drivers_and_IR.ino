#include <IRremote.h>

#define PWMA 3
#define PWMB 3
#define AIN1 6
#define AIN2 7
#define BIN1 5
#define BIN2 4
#define STBY 1
#define motorSpeedStep 50

#define FORWARD_KEY    0xFF18E7
#define BACKWARD_KEY   0xFF38C7
#define LEFT_KEY       0xFF10EF
#define RIGHT_KEY      0xFF5AA5
#define SPEED_UP_KEY   0xFFA857
#define SPEED_DOWN_KEY 0xFFE01F

int motorSpeed = 250;
IRrecv irrecv(3);
decode_results results;

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
  irrecv.enableIRIn();
  Serial.begin(9600);
  digitalWrite(STBY, HIGH);
}

void loop() {
  MoveForward(250);
  delay(1000);
  TurnLeft(250);
  delay(1000);
  MoveForward(250);
  delay(1000);
  MoveBackward(250);
  delay(1000);
}
