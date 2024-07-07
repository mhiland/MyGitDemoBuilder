import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from collections import Counter

with open('commits.txt', 'r') as file:
    dates = file.readlines()

dates = [date.strip() for date in dates]

commit_counts = Counter(dates)

df = pd.DataFrame(list(commit_counts.items()), columns=['date', 'count'])
df['date'] = pd.to_datetime(df['date'])

df.set_index('date', inplace=True)

date_range = pd.date_range(start=df.index.min(), end=df.index.max())
df = df.reindex(date_range, fill_value=0)

df['day'] = (df.index.dayofweek + 1) % 7 
df['week'] = df.index.to_period('W-SUN')

heatmap_data = df.pivot_table(values='count', index='day', columns='week', fill_value=0, aggfunc='sum')

sunday_row = heatmap_data.loc[0]
heatmap_data.loc[0] = sunday_row.shift(1, fill_value=0)

day_labels = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']

fig, ax = plt.subplots(figsize=(15, 5))
sns.heatmap(heatmap_data, cmap='Blues', ax=ax, cbar=True, yticklabels=day_labels)
ax.set_xlabel('Week')
ax.set_ylabel('Day of Week')
ax.set_title('GitHub Repository Activity Heatmap')
plt.show()
