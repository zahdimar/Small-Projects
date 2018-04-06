#define in1 3
#define in2 4
#define in3 5
#define in4 6

int motorOut[4] = {3, 4, 5, 6};
int pos = 0;
int motorData[8][4] = {
  {HIGH,HIGH,LOW,LOW}, 
  {LOW,HIGH,LOW,LOW}, 
  {LOW,HIGH,HIGH,LOW}, 
  {LOW,LOW,HIGH,LOW}, 
  {LOW,LOW,HIGH,HIGH}, 
  {LOW,LOW,LOW,HIGH}, 
  {HIGH,LOW,LOW,HIGH},
  {HIGH,LOW,LOW,LOW}
  }; 

void setup() {
  pinMode(in1, OUTPUT);
  pinMode(in2, OUTPUT);
  pinMode(in3, OUTPUT);
  pinMode(in4, OUTPUT);

  Serial.begin(9600);
}

void loop() {
  for (int i = 0; i< 4; i++) {
    digitalWrite(motorOut[i], motorData[pos][i]);
  }
  delayMicroseconds(1000);
  pos = (pos + 1) % 8;
}
