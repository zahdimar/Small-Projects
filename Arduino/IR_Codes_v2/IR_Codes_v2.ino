#include "IRLremote.h"
CHashIR IRLremote;
uint32_t IRdata;

#define IR_PIN 2

void setup() {
  Serial.begin(9600);
  IRLremote.begin(IR_PIN);
}

void loop() {
   if (IRLremote.available())  {
    auto data = IRLremote.read();
    Serial.print(F("Received value ")); Serial.println(data.command, HEX);
  }
}
