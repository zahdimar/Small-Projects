#include <IRremote.h>
IRrecv irrecv(2);

decode_results results;
int led1 = 0;
int led2 = 0;
int led3 = 0;

void setup() {
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);

  Serial.begin(9600);
  irrecv.enableIRIn();
}

void loop() {
  if (irrecv.decode( &results)) {
    
    Serial.println(results.value, HEX);

    if(results.value == 0xFF30CF)
      led1 = (led1 + 1) % 2;

    if(results.value == 0xFF18E7)
      led2 = (led2 + 1) % 2;

    if(results.value == 0xFF7A85)
      led3 = (led3 + 1) % 2;
    

    digitalWrite(3, led1);
    digitalWrite(4, led2);
    digitalWrite(5, led3);
    
    irrecv.resume();
  }
}
