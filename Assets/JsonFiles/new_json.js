{
  'eth_clients':
    [
      {
        'clients': 
          [
            {
              'idle': '9.10',
              'target_mac': '00:40:ad:91:be:a0', // 1
              'hostname': 'Eth1 iPhone',
              'IP_Address': '192.222.3.4',
            }
          ],
          'serial': '5054494e912ce94f', // Router
      },
      {
        'clients': [], 
        'serial': 'aw2311813001257' // Extender
        
      },
      {
        'clients': [], 
        'serial': 'aw2311813005151'
      } 
    ],

    'mesh_links': 
      [
        {
          'connected_to': 
            [
              {
                'rssi': -660, 
                'serial': 'aw2311813001257' // 2
              },
              {
                'rssi': -660, 
                'serial': 'aw2311813005151' // 3
              }
            ],

            'isMaster': True,
            'serial': '5054494e912ce94f',    // 4
            'hostname': 'Mesh1 iPhone',
            'IP_Address': '192.222.3.4'
          },
          {
            'connected_to': 
              [
                {
                  'rssi': -546, 
                  'serial': '5054494e912ce94f' // 5
                },
                {
                  'rssi': -546, 
                  'serial': 'aw2311813005151' // 6
                }
              ],

              'isMaster': False,
              'serial': 'aw2311813001257', // 7
              'hostname': 'Mesh2 iPhone',
              'IP_Address': '192.222.3.4'
            },
            {
              'connected_to': 
                [
                  {
                    'rssi': -570, 
                    'serial': '5054494e912ce94f' // 8
                  },
                  {
                    'rssi': -570, 
                    'serial': 'aw2311813001257' // 9
                  }
                ],

              'isMaster': False,
              'serial': 'aw2311813005151', // 10
              'hostname': 'Mesh3 iPhone',
              'IP_Address': '192.222.3.4'
            }
          ],

    'sta_clients': 
      [
        {
          'clients': 
          [
            {
              'rssi': -850,
              'rxpr': 0,
              'target_mac': '00:20:00:be:79:e2',
              'txpr': 0,
              'hostname': 'Sta1 iPhone',
              'IP_Address': '192.222.3.4'
            },
            {
              'rssi': -500,
              'rxpr': 144,
              'target_mac': 'b0:05:94:40:23:47',
              'txpr': 144,
              'hostname': 'Sta2 iPhone',
              'IP_Address': '192.222.3.4',
            }
          ],

          'serial': '5054494e912ce94f',
        }
      ]
    }