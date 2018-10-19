#define PWMA 11
#define PWMB 10
#define AIN1 6
#define AIN2 7
#define BIN1 5
#define BIN2 4
#define STBY 13
int mooveDelay = 3000;
int motorSpeed = 500;

void setup() {
  pinMode(PWMA, OUTPUT);
  pinMode(PWMB, OUTPUT);
  pinMode(AIN1, OUTPUT);
  pinMode(AIN2, OUTPUT);
  pinMode(BIN1, OUTPUT);
  pinMode(BIN2, OUTPUT);
  pinMode(STBY, OUTPUT);
  digitalWrite(STBY, HIGH);
}

void loop() {
  digitalWrite(AIN1, LOW);
  digitalWrite(AIN2, HIGH);
  digitalWrite(BIN1, LOW);
  digitalWrite(BIN2, HIGH);
  analogWrite(PWMB, motorSpeed);
  analogWrite(PWMA, motorSpeed);
  delay(mooveDelay);

  digitalWrite(AIN1, HIGH);
  digitalWrite(AIN2, LOW);
  digitalWrite(BIN1, HIGH);
  digitalWrite(BIN2, LOW);
  analogWrite(PWMB, motorSpeed);
  analogWrite(PWMA, motorSpeed);
  delay(mooveDelay);

  digitalWrite(STBY, LOW);
  delay(mooveDelay);
  digitalWrite(STBY, HIGH);
}
