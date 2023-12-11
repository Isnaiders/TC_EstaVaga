#include <Ultrasonic.h>

#define TRIG_PIN 12
#define ECHO_PIN 13

//Arduino UNO (Id da vaga do Shopping Tatuape(vacancyId = 1E2FA6AA-D5D6-47FF-A56F-21550034B364))
//Arduino UNO WIFI (Id da Vaga do Hospital Amil(vacancyId = C2790CC5-BBD2-495E-9E4F-038AA227827B))
char vacancyId[] = "C2790CC5-BBD2-495E-9E4F-038AA227827B";
int isBusy = 0;

Ultrasonic ultrasonic(TRIG_PIN, ECHO_PIN);

void setup() {
  Serial.begin(9600);
}

void loop() {
  float cmMsec;
  long microsec = ultrasonic.timing();
  cmMsec = ultrasonic.convert(microsec, Ultrasonic::CM);

  if (cmMsec<90){
    isBusy = 1;
  } else{
    isBusy = 0;
  }

  Serial.print("{\"vacancyId\": \"");
  Serial.print(vacancyId);
  Serial.print("\",\"isBusy\": ");
  Serial.print(isBusy);
  Serial.println("}");
}