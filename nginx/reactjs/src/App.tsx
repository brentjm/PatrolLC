import React from 'react';
import './App.css';
import './user-interface/main/Main';

const App = ({Main}) => {
  return (
    <div className="App">
      <header className="App-header">
        <p>
          Patrol Interface
        </p>
      </header>
      <body>
        {Main}
      </body>
    </div>
  );
}

export default App;
