import requests
from bs4 import BeautifulSoup
from datetime import datetime
import os

def fetch_trending():
    url = "https://github.com/trending"
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36'
    }
    response = requests.get(url, headers=headers)
    soup = BeautifulSoup(response.text, 'html.parser')
    
    repos = []
    articles = soup.select('article.Box-row')
    print(f"Found {len(articles)} repositories")
    
    for article in articles[:50]:
        try:
            repo_link = article.select_one('h2.h3 a')
            name = repo_link['href'].strip('/')
            
            description = article.select_one('p')
            description = description.get_text(strip=True) if description else "暂无描述"
            
            stats = article.select('a.Link--muted')
            stars = stats[0].get_text(strip=True) if len(stats) > 0 else "0"
            forks = stats[1].get_text(strip=True) if len(stats) > 1 else "0"
            
            today_stars = article.select_one('span.d-inline-block.float-sm-right')
            today_stars = today_stars.get_text(strip=True) if today_stars else "0"
            
            language_span = article.select_one('span[itemprop="programmingLanguage"]')
            language = language_span.get_text(strip=True) if language_span else "未知语言"
            
            url = f"https://github.com/{name}"
            
            repos.append({
                'name': name,
                'description': description,
                'url': url,
                'stars': stars.replace(',', ''),
                'forks': forks.replace(',', ''),
                'today_stars': today_stars.replace('stars today', '').strip(),
                'language': language
            })
            print(f"Successfully processed: {name}")
            
        except Exception as e:
            print(f"Error processing repository: {str(e)}")
            continue
    
    print(f"Successfully processed {len(repos)} repositories")
    return repos

if __name__ == "__main__":
    current_time = datetime.now().strftime('%Y-%m-%d %H:%M')
    current_time_file = datetime.now().strftime('%Y%m%d%H%M')
    
    print(f"Current time: {current_time}")
    print(f"System timezone: {os.environ.get('TZ', 'not set')}")
    
    trending_repos = fetch_trending()
    
    with open(f'Action_Trending/trending-{current_time_file}.md', 'w', encoding='utf-8') as f:
        f.write(f'# GitHub Trending 热门项目 ({current_time})\n\n')
        f.write('*自动更新时间：每天早7:00、中午12:00、晚22:00*\n\n')
        f.write(f'*共 {len(trending_repos)} 个项目*\n\n')
        
        if trending_repos:
            for idx, repo in enumerate(trending_repos, 1):
                f.write(f'## {idx}. {repo["name"]}\n')
                f.write(f'- 📝 描述：{repo["description"]}\n')
                f.write(f'- ⭐ Stars：{repo["stars"]}\n')
                f.write(f'- 🔱 Forks：{repo["forks"]}\n')
                f.write(f'- 📈 今日新增：{repo["today_stars"]}\n')
                f.write(f'- 💻 主要语言：{repo["language"]}\n')
                f.write(f'- 🔗 项目链接：[点击访问]({repo["url"]})\n\n')
        else:
            f.write('暂时没有获取到任何项目信息。\n')
        
        f.write('\n---\n*更多项目请访问 [GitHub Trending](https://github.com/trending)*')