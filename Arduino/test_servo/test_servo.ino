#include <Servo.h>

Servo servo1;

void setup() {
  pinMode(A5, INPUT);
  servo1.attach(5);
  Serial.begin(9600);
}


void loop() {
  int potent = analogRead(5);
  Serial.println(potent);
  potent = map(potent, 0, 1024, 0, 180);
  servo1.write(potent);
  delay(2);

}
