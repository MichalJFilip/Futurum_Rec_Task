import { useEffect, useState } from 'react';
import API from './api';
import CampaignForm from './components/CampaignForm';
import React from 'react';


function App() {
  const [campaigns, setCampaigns] = useState([]);
  const [editing, setEditing] = useState(null);

  const fetchCampaigns = async () => {
    const res = await API.get('/campaigns');
    setCampaigns(res.data);
  };

  const handleSave = async (data) => {
    if (editing) {
      await API.put(`/campaigns/${data.id}`, data);
    } else {
      await API.post('/campaigns', data);
    }
    setEditing(null);
    fetchCampaigns();
  };

  const handleDelete = async (id) => {
    await API.delete(`/campaigns/${id}`);
    fetchCampaigns();
  };

  useEffect(() => {
    fetchCampaigns();
  }, []);

  return (
    <div className="p-4 max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Campaigns</h1>
      <CampaignForm onSubmit={handleSave} initialData={editing} />

      <ul className="mt-6 space-y-4">
        {campaigns.map(c => (
          <li key={c.id} className="border p-4 rounded shadow">
            <h2 className="text-xl font-semibold">{c.name}</h2>
            <p>Bid: ${c.bidAmount} | Fund: ${c.campaignFund}</p>
            <p>Status: {c.status} | Town: {c.town} | Radius: {c.radius} km</p>
            <p>Keywords: {c.keywords.join(', ')}</p>
            <div className="space-x-2 mt-2">
              <button onClick={() => setEditing(c)} className="text-blue-600">Edit</button>
              <button onClick={() => handleDelete(c.id)} className="text-red-600">Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
