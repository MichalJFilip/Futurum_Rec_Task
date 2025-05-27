import { useState, useEffect } from 'react';
import React from 'react';


const TOWNS = ['Cracow', 'Warsaw', 'Łódź', 'Poznań', 'Wrocław'];
const STATUSES = ['On', 'Off'];
const KEYWORDS = ['sale', 'auction', 'perfume', 'clothes', 'technologies', 'games'];

export default function CampaignForm({ onSubmit, initialData = null }) {
  const [form, setForm] = useState({
    name: '',
    bidAmount: 0,
    campaignFund: 0,
    status: 'On',
    town: 'Cracow',
    radius: 0,
    keywords: [],
  });

  useEffect(() => {
    if (initialData) setForm(initialData);
  }, [initialData]);

  const handleChange = e => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleKeywordToggle = keyword => {
    setForm(prev => ({
      ...prev,
      keywords: prev.keywords.includes(keyword)
        ? prev.keywords.filter(k => k !== keyword)
        : [...prev.keywords, keyword],
    }));
  };

  const submit = e => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    <form onSubmit={submit} className="space-y-4">
      <p>Campaign name:</p>
      <input name="name" value={form.name} onChange={handleChange} placeholder="Campaign name" required className="input" />
      <p>Bid amount:</p>
      <input type="number" name="bidAmount" value={form.bidAmount} onChange={handleChange} placeholder="Bid amount" min={0.01} step="0.01" required className="input" />
      <p>Campaign fund:</p>
      <input type="number" name="campaignFund" value={form.campaignFund} onChange={handleChange} placeholder="Campaign fund" min={0} step="0.01" required className="input" />
      <p>Radius:</p>
      <input type="number" name="radius" value={form.radius} onChange={handleChange} placeholder="Radius (km)" min={1} required className="input" />

      <p>Status:</p>
      <select name="status" value={form.status} onChange={handleChange} className="input">
        {STATUSES.map(s => <option key={s}>{s}</option>)}
      </select>

      <p>Town:</p>
      <select name="town" value={form.town} onChange={handleChange} className="input">
        {TOWNS.map(t => <option key={t}>{t}</option>)}
      </select>

      <p>Keywords:</p>
      <div className="flex flex-wrap gap-2 text-black">
        {KEYWORDS.map(k => (
          <button type="button" key={k}
            onClick={() => handleKeywordToggle(k)}
            className={`px-2 py-1 border rounded ${form.keywords.includes(k) ? 'bg-blue-500 text-white' : 'bg-gray-100'}`}>
            {k}
          </button>
        ))}
      </div>

      <button type="submit" className="bg-green-600 text-white px-4 py-2 rounded">Save</button>
    </form>
  );
}
