import requests
from bs4 import BeautifulSoup
import json
from datetime import datetime

def fetch_trending():
    url = "https://github.com/trending"
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36'
    }
    response = requests.get(url, headers=headers)
    soup = BeautifulSoup(response.text, 'html.parser')
    
    repos = []
    articles = soup.select('article.Box-row')
    
    for article in articles[:50]:  # 获取前50个仓库
        try:
            name = article.select_one('h2 a').get_text(strip=True)
            repo_relative_url = article.select_one('h2 a')['href'].strip('/')
            description = article.select_one('p')
            description = description.get_text(strip=True) if description else "暂无描述"
            
            # 获取 star 数和 fork 数
            stats = article.select('span.d-inline-block')
            stars = stats[0].get_text(strip=True) if len(stats) > 0 else "0"
            forks = stats[1].get_text(strip=True) if len(stats) > 1 else "0"
            
            # 获取今日新增 star 数
            today_stars = article.select_one('.float-sm-right')
            today_stars = today_stars.get_text(strip=True) if today_stars else "0"
            
            # 获取主要编程语言
            language = article.select_one('[itemprop="programmingLanguage"]')
            language = language.get_text(strip=True) if language else "未知语言"
            
            url = f"https://github.com/{repo_relative_url}"
            
            repos.append({
                'name': name,
                'description': description,
                'url': url,
                'stars': stars,
                'forks': forks,
                'today_stars': today_stars,
                'language': language
            })
        except Exception as e:
            print(f"Error processing repository: {str(e)}")
            continue
    
    return repos

# 获取趋势项目
trending_repos = fetch_trending()

# 保存结果
current_time = datetime.now().strftime('%Y-%m-%d %H:%M')

with open(f'Action_Trending/trending-{datetime.now().strftime("%Y%m%d%H%M")}.md', 'w', encoding='utf-8') as f:
    f.write(f'# GitHub Trending 热门项目 ({current_time})\n\n')
    f.write('*自动更新时间：每天早7:00、中午12:00、晚22:00*\n\n')
    
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
