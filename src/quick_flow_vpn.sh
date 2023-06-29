#!/bin/bash

api="https://api.quickflowvpn.com/api/v1"
user_agent="okhttp/4.10.0"
uuid=$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 8)-$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 4)-4$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 3)-$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 4)-$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 12)

function register() {
	curl --request POST \
		--url "$api/reg" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--data '{
			"uuid": "'$uuid'",
			"appVersionCode": 3,
			"appVersionName": "1.1.1",
			"androidVersion": "9",
			"androidOsVersion": 28,
			"model": "SHARK KTUS-H0",
			"brand": "blackshark",
			"productName": "SHARK KTUS-H0",
			"manufacture": "blackshark",
			"device": "gracelte",
			"deviceLanguage": "ru",
			"timeZone": "GMT+03:00",
			"appId": "com.quickflowvpn",
			"screenDensityDpi": 480,
			"screenHeightPx": 1920,
			"screenWidthPx": 1080,
			"deviceSerialNumber": "'$(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 16 | head -n 1 | tr '[:upper:]' '[:lower:]')'",
			"networkOperator": "'$(shuf -i 100000-999999 -n 1)'"
		}'
}

function get_servers() {
	curl --request GET \
		--url "$api/servers?uuid=$uuid" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" 
}
