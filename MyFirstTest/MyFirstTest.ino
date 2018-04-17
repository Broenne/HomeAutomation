/*
 Name:		MyFirstTest.ino
 Created:	11/30/2017 4:35:15 AM
 Author:	Marcus
*/

#include "dht.h"
#include "dht.h"

// https://www.hivemq.com/blog/mqtt-client-library-encyclopedia-arduino-pubsubclient/
#include <SPI.h>
#include <Ethernet.h>
#include <PubSubClient.h>


//#include <dht.h>
dht DHT;
int sensorPin = 13;


EthernetClient ethClient;
PubSubClient mqttClient(ethClient);

// the setup function runs once when you press reset or power the board
void setup() {
	//pinMode(LED_BUILTIN, OUTPUT);;
	pinMode(sensorPin, INPUT);;
	Serial.begin(115200);
}

//InternalLed internalLed;

// the loop function runs over and over again forever
void loop() {
	
	/*int xxx  = digitalPinToBitMask(sensorPin);
	Serial.print(xxx);*/

	int chk = DHT.read11(sensorPin); //Status holen
	switch (chk)
	{
		//Wenn alles OK ist.
	case DHTLIB_OK:
		break;
		//Bei einem Checksummenfehler
	case DHTLIB_ERROR_CHECKSUM:
		Serial.print("Checksummenfehler");
		break;
		//Bei einer Zeitüberschreitung der Anforderung.
	case DHTLIB_ERROR_TIMEOUT:
		Serial.print("Zeitüberschreitung");
		break;
	default:
		break;
	}

	//Werte ausgeben
	Serial.print("Temperatur: ");
	Serial.print(DHT.temperature, 1); //Die Temperatur auslesen.
	Serial.println("C");
	Serial.print("Luftfeuchtigkeit: ");
	Serial.print(DHT.humidity, 1);  // Die Luftfeuchtigkeit auslesen.
	Serial.println("%");
	Serial.println("");

	//Eine Pause von 2sek. der Sensor DHT11 stellt alle 2sek neue Werte zur Verfügung daher 
	//ist ein Wert < 2sek. nicht möglich bzw. unnötig.
	delay(1000);
	//digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
	//delay(1000);                       // wait for a second
	//Serial.println("test");
	//digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
	//delay(1000);
}

