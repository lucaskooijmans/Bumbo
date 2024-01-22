import RPi.GPIO as GPIO
import socket
import requests
import threading
import time
from mfrc522 import SimpleMFRC522
from flask import Flask, jsonify
from datetime import datetime

app = Flask(__name__)

reader = SimpleMFRC522()

writing_state = True
writing_start_time = 0


def set_writing_state(value):
    global writing_state
    global writing_start_time
    writing_state = value
    if value:
        writing_start_time = time.time()

def read_rfid_loop():
    while not writing_state:
        try:
            id, text = reader.read()
            api_url = 'http://192.168.2.30:8000/api/clock'
            current_time = datetime.now()
            formatted_time = current_time.strftime('%Y-%m-%d %H:%M:%S')
            data = {'employeeId': text, 'timestampString': formatted_time}
            response = requests.post(api_url, params=data)
            # Wait for the response and print it
            if response.status_code == 200:
                print(f"Server response: {response.text}")
                time.sleep(3)
            else:
                print(f"Error: {response.status_code}, {response.text}")
        except Exception as e:
            print(f"An error occurred: {str(e)}")


@app.route('/api/write-rfid/<int:user_id>', methods=['POST'])
def write_rfid(user_id):
    set_writing_state(False)
    print("test")
    writting = True
    try:
        set_writing_state(True)
        print(f"Writing user ID {user_id} to RFID...")
        employeeId = str(user_id)
        id, text = reader.read()
        print(f"Current to RFID: {text}")
        while writting:
            try:
                print("writing")
                reader.write(employeeId)
                text = reader.read()
                writting = False
            except Exception as error:
                print("Error while writing!")
                print(error)
        print("Write operation completed")
        id, test = reader.read()
        print(f"Written to RFID: {test}")
        set_writing_state(False)
        GPIO.cleanup()
        return jsonify({'success': True, 'message': f'User ID {user_id} written to RFID.'})
    except Exception as e:
        print(f"Error during RFID write operation: {str(e)}")
        set_writing_state(False)
        GPIO.cleanup()
        return jsonify({'success': False, 'error': str(e)})

if __name__ == '__main__':
    set_writing_state(False)

    rfid_thread = threading.Thread(target=read_rfid_loop)
    rfid_thread.start()

    app.run(host='0.0.0.0', port=5000)


