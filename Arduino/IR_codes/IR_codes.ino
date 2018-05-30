#include <IRremote.h>
IRrecv irrecv(2);

decode_results results;
int currentState = 0;
void setup() {
  Serial.begin(9600);
  pinMode(13, OUTPUT);
  irrecv.enableIRIn();
}

void loop() {
  if (irrecv.decode(&results)) {
    Serial.println(results.value, HEX);
    currentState = (currentState + 1) % 2 ;
    digitalWrite(13, currentState);
    irrecv.resume();
  }
}
